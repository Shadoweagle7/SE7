namespace SE7.Events
{
    public abstract class AsyncEvent : IEvent<Func<EventArgs, Task>>
    {
        private readonly List<Func<EventArgs, Task>> Callbacks = [];

        public void AddCallback(Func<EventArgs, Task> @delegate) => Callbacks.Add(@delegate);

        /// <summary>
        /// Raises this event by calling all callbacks subscribed to this event all at once awaits them all
        /// with <see cref="Task.WhenAll(IEnumerable{Task})"/>.
        /// </summary>
        /// <param name="eventArgs">The arguments to pass to each callback.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public async Task RaiseParallelAsync(EventArgs eventArgs)
        {
            await Task.WhenAll(Callbacks.Select(c => c(eventArgs)));
        }

        /// <summary>
        /// Raises <typeparamref name="TAsyncEvent"/> by calling and awaiting each of its callbacks one by one.
        /// </summary>
        /// <param name="eventArgs">The arguments to pass to each callback.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
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
