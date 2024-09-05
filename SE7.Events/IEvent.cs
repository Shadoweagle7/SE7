namespace SE7.Events
{
    public interface IEvent;

    public interface IEvent<TDelegate, TEvent> : IEvent where TDelegate : Delegate where TEvent : IEvent<TDelegate, TEvent>
    {
        public void AddCallback(TDelegate @delegate);
        public void RemoveCallback(TDelegate @delegate);

        public static abstract TEvent operator +(TEvent @event, TDelegate @delegate);
        public static abstract TEvent operator -(TEvent @event, TDelegate @delegate);
    }
}
