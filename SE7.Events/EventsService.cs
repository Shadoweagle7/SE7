namespace SE7.Events
{
    public class EventsService
    {
        private readonly Dictionary<Type, IEvent> Events = [];

        public bool TryRegisterEvent<TEvent>() where TEvent : Event, new() => Events.TryAdd(typeof(TEvent), new TEvent());
        public bool TryRegisterAsyncEvent<TAsyncEvent>() where TAsyncEvent : Event, new() =>
            Events.TryAdd(typeof(TAsyncEvent), new TAsyncEvent());

        public bool TrySubscribeEvent<TEvent>(Action<EventArgs> callback) where TEvent : Event, new()
        {
            if (Events.TryGetValue(typeof(TEvent), out var @event) && @event is Event e)
            {
                e.AddCallback(callback);

                return true;
            }

            return false;
        }

        public bool TryUnsubscribeEvent<TEvent>(Action<EventArgs> callback) where TEvent : Event, new()
        {
            if (Events.TryGetValue(typeof(TEvent), out var @event) && @event is Event e)
            {
                e.RemoveCallback(callback);

                return true;
            }

            return false;
        }

        public bool TrySubscribeAsyncEvent<TAsyncEvent>(Func<EventArgs, Task> callback) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                ae.AddCallback(callback);

                return true;
            }

            return false;
        }

        public bool TryUnubscribeAsyncEvent<TAsyncEvent>(Func<EventArgs, Task> callback) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                ae.RemoveCallback(callback);

                return true;
            }

            return false;
        }

        public bool TryRaiseEvent<TEvent>(EventArgs eventArgs) where TEvent : Event, new()
        {
            if (Events.TryGetValue(typeof(TEvent), out var @event) && @event is Event e)
            {
                e.Raise(eventArgs);

                return true;
            }

            return false;
        }

        public async Task<bool> TryRaiseAsyncEventParallelAsync<TAsyncEvent>(EventArgs eventArgs) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                await ae.RaiseParallelAsync(eventArgs);

                return true;
            }

            return false;
        }

        public async Task<bool> TryRaiseAsyncEventSequentiallyAsync<TAsyncEvent>(EventArgs eventArgs) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                await ae.RaiseSequentiallyAsync(eventArgs);

                return true;
            }

            return false;
        }
    }
}
