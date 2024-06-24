using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Events
{
    public class AsyncEvent : IEvent<Func<EventArgs, Task>>
    {
        private readonly List<Func<EventArgs, Task>> Callbacks = [];

        public void AddCallback(Func<EventArgs, Task> @delegate) => Callbacks.Add(@delegate);

        public async Task RaiseParallelAsync(EventArgs eventArgs)
        {
            var callbackTasks = new List<Task>();

            foreach (var callback in Callbacks)
            {
                callbackTasks.Add(callback(eventArgs));
            }
            
            await Task.WhenAll(callbackTasks);
        }

        public async Task RaiseSequentiallyAsync(EventArgs eventArgs)
        {
            foreach (var callback in Callbacks)
            {
                await callback(eventArgs);
            }
        }

        public void RemoveCallback(Func<EventArgs, Task> @delegate) => Callbacks.Remove(@delegate);
    }
}
