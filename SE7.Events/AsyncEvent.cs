namespace SE7.Events
{
    public abstract class AsyncEvent : IEvent<Func<EventArgs, Task>, AsyncEvent>
    {
        private readonly List<Func<EventArgs, Task>> Callbacks = [];

        public void AddCallback(Func<EventArgs, Task> @delegate) => Callbacks.Add(@delegate);

        public async Task RaiseParallelAsync(EventArgs eventArgs)
        {
            await Task.WhenAll(Callbacks.Select(c => c(eventArgs)));
        }

        public async Task RaiseSequentiallyAsync(EventArgs eventArgs)
        {
            foreach (var callback in Callbacks)
            {
                await callback(eventArgs);
            }
        }

        public void RemoveCallback(Func<EventArgs, Task> @delegate) => Callbacks.Remove(@delegate);

        public static AsyncEvent operator +(AsyncEvent @event, Func<EventArgs, Task> @delegate)
        {
            @event.AddCallback(@delegate);

            return @event;
        }

        public static AsyncEvent operator -(AsyncEvent @event, Func<EventArgs, Task> @delegate)
        {
            @event.RemoveCallback(@delegate);

            return @event;
        }
    }
}
