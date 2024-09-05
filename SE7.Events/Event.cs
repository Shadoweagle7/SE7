namespace SE7.Events
{
    public abstract class Event : IEvent<Action<EventArgs>, Event>
    {
        private readonly List<Action<EventArgs>> Callbacks = [];

        public void AddCallback(Action<EventArgs> @delegate) => Callbacks.Add(@delegate);
        
        public void Raise(EventArgs eventArgs)
        {
            foreach (var callback in Callbacks)
            {
                callback(eventArgs);
            }
        }

        public void RemoveCallback(Action<EventArgs> @delegate) => Callbacks.Remove(@delegate);

        public static Event operator +(Event @event, Action<EventArgs> @delegate)
        {
            @event.AddCallback(@delegate);

            return @event;
        }

        public static Event operator -(Event @event, Action<EventArgs> @delegate)
        {
            @event.RemoveCallback(@delegate);

            return @event;
        }
    }
}
