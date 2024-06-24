using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Threading.Tasks
{
#error TODO: Pretty much copy-pasted from WhenAllPromise. Need to figure out what to do here

    internal class WhenSinglePromise : Task
    {
        /// <summary>Either a single faulted/canceled task, or a list of faulted/canceled tasks.</summary>
        private object? _failedOrCanceled;
        /// <summary>The number of tasks remaining to complete.</summary>
        private int _remainingToComplete;

        internal WhenSinglePromise(ReadOnlySpan<Task> tasks)
        {
            Debug.Assert(tasks.Length != 0, "Expected a non-zero length task array");

            // Throw if any of the provided tasks is null. This is best effort to inform the caller
            // they've made a mistake.  If between the time we check for nulls and the time we hook
            // up callbacks one of the entries is changed from non-null to null, we'll just ignore
            // the null at that point; any such use (e.g. calling WhenAll with an array that's mutated
            // concurrently with the synchronous call to WhenAll) is erroneous.
            foreach (Task task in tasks)
            {
                if (task is null)
                {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Task_MultiTaskContinuation_NullTask, ExceptionArgument.tasks);
                }
            }

            if (TplEventSource.Log.IsEnabled())
            {
                TplEventSource.Log.TraceOperationBegin(Id, "Task.WhenAll", 0);
            }

            if (s_asyncDebuggingEnabled)
            {
                AddToActiveTasks(this);
            }

            _remainingToComplete = tasks.Length;

            foreach (Task task in tasks)
            {
                if (task is null || task.IsCompleted)
                {
                    Invoke(task); // short-circuit the completion action, if possible
                }
                else
                {
                    task.AddCompletionAction(this); // simple completion action
                }
            }
        }

        public void Invoke(Task? completedTask)
        {
            if (TplEventSource.Log.IsEnabled())
            {
                TplEventSource.Log.TraceOperationRelation(Id, CausalityRelation.Join);
            }

            if (completedTask is not null)
            {
                if (completedTask.IsWaitNotificationEnabled)
                {
                    SetNotificationForWaitCompletion(enabled: true);
                }

                if (!completedTask.IsCompletedSuccessfully)
                {
                    // Try to store the completed task as the first that's failed or faulted.
                    if (Interlocked.CompareExchange(ref _failedOrCanceled, completedTask, null) != null)
                    {
                        // There was already something there.
                        while (true)
                        {
                            object? failedOrCanceled = _failedOrCanceled;
                            Debug.Assert(failedOrCanceled is not null);

                            // If it was a list, add it to the list.
                            if (_failedOrCanceled is List<Task> list)
                            {
                                lock (list)
                                {
                                    list.Add(completedTask);
                                }
                                break;
                            }

                            // Otherwise, it was a Task. Create a new list containing that task and this one, and store it in.
                            Debug.Assert(failedOrCanceled is Task, $"Expected Task, got {failedOrCanceled}");
                            if (Interlocked.CompareExchange(ref _failedOrCanceled, new List<Task> { (Task)failedOrCanceled, completedTask }, failedOrCanceled) == failedOrCanceled)
                            {
                                break;
                            }

                            // We lost the race, which means we should loop around one more time and it'll be a list.
                            Debug.Assert(_failedOrCanceled is List<Task>);
                        }
                    }
                }
            }

            // Decrement the count, and only continue to complete the promise if we're the last one.
            if (Interlocked.Decrement(ref _remainingToComplete) == 0)
            {
                object? failedOrCanceled = _failedOrCanceled;
                if (failedOrCanceled is null)
                {
                    if (TplEventSource.Log.IsEnabled())
                    {
                        TplEventSource.Log.TraceOperationEnd(Id, AsyncCausalityStatus.Completed);
                    }

                    if (s_asyncDebuggingEnabled)
                    {
                        RemoveFromActiveTasks(this);
                    }

                    bool completed = TrySetResult();
                    Debug.Assert(completed);
                }
                else
                {
                    // Set up some accounting variables
                    List<ExceptionDispatchInfo>? observedExceptions = null;
                    Task? canceledTask = null;

                    void HandleTask(Task task)
                    {
                        if (task.IsFaulted)
                        {
                            (observedExceptions ??= new()).AddRange(task.GetExceptionDispatchInfos());
                        }
                        else if (task.IsCanceled)
                        {
                            canceledTask ??= task; // use the first task that's canceled
                        }
                    }

                    // Loop through the completed or faulted tasks:
                    //   If any one of them faults, the result will be faulted
                    //   If none fault, but at least one is canceled, the result will be canceled
                    if (failedOrCanceled is List<Task> list)
                    {
                        foreach (Task task in list)
                        {
                            HandleTask(task);
                        }
                    }
                    else
                    {
                        Debug.Assert(failedOrCanceled is Task);
                        HandleTask((Task)failedOrCanceled);
                    }

                    if (observedExceptions != null)
                    {
                        Debug.Assert(observedExceptions.Count > 0, "Expected at least one exception");

                        // We don't need to TraceOperationCompleted here because TrySetException will call Finish and we'll log it there

                        TrySetException(observedExceptions);
                    }
                    else if (canceledTask != null)
                    {
                        TrySetCanceled(canceledTask.CancellationToken, canceledTask.GetCancellationExceptionDispatchInfo());
                    }
                }

                Debug.Assert(IsCompleted);
            }

            Debug.Assert(_remainingToComplete >= 0, "Count should never go below 0");
        }

        public bool InvokeMayRunArbitraryCode => true;
    }
}
