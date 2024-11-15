namespace SE7.Events
{
    /// <summary>
    /// Specifies that a class implementing this interface is an event.
    /// </summary>
    public interface IEvent;

    /// <summary>
    /// Specifies that a class implementing this interface is an event.
    /// </summary>
    /// <typeparam name="TDelegate">The type of the callbacks that this event will invoke.</typeparam>
    public interface IEvent<TDelegate> : IEvent where TDelegate : Delegate
    {
        /// <summary>
        /// Subscribes a callback to this event.
        /// </summary>
        /// <param name="delegate">The callback to subscribe to this event.</param>
        public void AddCallback(TDelegate @delegate);
        
        /// <summary>
        /// Removes a callback from this event.
        /// </summary>
        /// <param name="delegate">The callback to remove from this event.</param>
        public bool TryRemoveCallback(TDelegate @delegate);
    }
}
