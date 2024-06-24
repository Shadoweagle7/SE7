using SE7.Events;
using SE7.Threading.Events;
using System.Collections.Concurrent;

namespace SE7.Threading
{
    public sealed class ThreadPool : IDisposable
    {
        private record class WorkItem(Action<object?> Action, object? Argument);

        internal class WorkerThread
        {
            private readonly Thread Thread;
            public AutoResetEvent AwakenEvent { get; } = new(false);
            public bool Awake { get; private set; } = true;

            public int Id => Thread.ManagedThreadId;

            public WorkerThread(ThreadPool threadPool)
            {
                Thread = new(WorkerFunc);

                Thread.Start(KeyValuePair.Create(threadPool, this));
            }

            public void Join() => Thread.Join();

            private static void WorkerFunc(object? obj)
            {
                var (threadPool, workerThread) = (KeyValuePair<ThreadPool, WorkerThread>)obj!;

                threadPool.IsRunning = true;

                while (threadPool.IsRunning)
                {
                    try
                    {
                        workerThread.Awake = false;
                        workerThread.AwakenEvent.WaitOne();
                        workerThread.Awake = true;

                        if (threadPool.WorkItems.TryDequeue(out var workItem))
                        {
                            workItem.Action(workItem.Argument);
                        }
                    }
                    catch (Exception e)
                    {
                        threadPool.EventsService.TryRaiseEvent<ExceptionThrownInThreadPoolEvent>(
                            new ExceptionThrownInThreadPoolEventArgs(e)
                        );
                    }
                }
            }
        }

        private const int AutoResetEventCheckMillisecondsTimeout = 1;
        private bool IsRunning;
        internal readonly Dictionary<int, WorkerThread> Threads;
        private readonly ConcurrentQueue<WorkItem> WorkItems = [];
        private readonly EventsService EventsService;
        public Guid Id { get; } = Guid.NewGuid();
        public int Size => Threads.Count;
        public static int MaxThreads => Environment.ProcessorCount;

        private void AddWorkerThread()
        {
            var workerThread = new WorkerThread(this);

            Threads.Add(workerThread.Id, workerThread);
        }

        public ThreadPool(EventsService eventsService, int size = 1)
        {
            if (size > MaxThreads)
            {
                throw new ArgumentException("Too many threads requested.");
            }

            eventsService.TryRegisterEvent<ExceptionThrownInThreadPoolEvent>();

            Threads = new(size);
            EventsService = eventsService;
        }

        internal ThreadPool(EventsService eventsService, IDictionary<int, WorkerThread> threads)
        {
            eventsService.TryRegisterEvent<ExceptionThrownInThreadPoolEvent>();

            Threads = new(threads);
            EventsService = eventsService;
        }

        public void QueueUserWorkItem(Action<object?> action, object? argument)
        {
            WorkItems.Enqueue(new WorkItem(action, argument));

            if (!Threads.Any(t => !t.Value.Awake))
            {
                if (Threads.Count < MaxThreads)
                {
                    AddWorkerThread();
                }
                else
                {
                    throw new InvalidOperationException("Exceeded maximum thread capacity for this Thread Pool.");
                }
            }
        }

        public void Dispose()
        {
            foreach (var kvp in Threads)
            {
                IsRunning = false;
                kvp.Value.AwakenEvent.Set(); // One final flush of events
                kvp.Value.Join();
            }
        }
    }
}
