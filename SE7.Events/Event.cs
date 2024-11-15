namespace SE7.Events
{
    /// <summary>
    /// Represents a synchronous event.
    /// </summary>
    public abstract class Event : IEvent<Action<EventArgs>>
    {
        private readonly List<Action<EventArgs>> Callbacks = [];

        public void AddCallback(Action<EventArgs> @delegate) => Callbacks.Add(@delegate);

        /// <summary>
        /// Raises this event, calling all callbacks subscribed to this event.
        /// </summary>
        /// <param name="eventArgs">The arguments to pass to each callback.</param>
        public void Raise(EventArgs eventArgs)
        {
            foreach (var callback in Callbacks)
            {
                callback(eventArgs);
            }
        }

        public void RemoveCallback(Action<EventArgs> @delegate) => Callbacks.Remove(@delegate);
    }
}
