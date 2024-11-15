using SE7.Events.Services.Interfaces;

namespace SE7.Events.Services.Implementations
{
    /// <summary>
    /// A simple events service that can be injected to services such that events can easily be raised in between services.
    /// </summary>
    internal sealed class EventsService : IEventsService
    {
        private readonly Dictionary<Type, IEvent> Events = [];

        /// <summary>
        /// Attempts to registers an event to the service.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <returns><see langword="true"/> if the event was successfully registered; <see langword="false"/> otherwise.</returns>
        public bool TryRegisterEvent<TEvent>() where TEvent : Event, new() => Events.TryAdd(typeof(TEvent), new TEvent());

        /// <summary>
        /// Attempts to registers an asynchronous event to the service.
        /// </summary>
        /// <typeparam name="TAsyncEvent">The type of the asynchronous event.</typeparam>
        /// <returns><see langword="true"/> if the asynchronous event was successfully registered; <see langword="false"/> otherwise.</returns>
        public bool TryRegisterAsyncEvent<TAsyncEvent>() where TAsyncEvent : AsyncEvent, new() =>
            Events.TryAdd(typeof(TAsyncEvent), new TAsyncEvent());

        /// <summary>
        /// Attempts to subscribe <paramref name="callback"/> to the <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to subscribe <paramref name="callback"/> to.</typeparam>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns><see langword="true"/> if the callback was successfully subscribed; <see langword="false"/> otherwise.</returns>
        public bool TrySubscribeToEvent<TEvent>(Action<EventArgs> callback) where TEvent : Event, new()
        {
            if (Events.TryGetValue(typeof(TEvent), out var @event) && @event is Event e)
            {
                e.AddCallback(callback);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to unsubscribe <paramref name="callback"/> from <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to unsubscribe <paramref name="callback"/> from.</typeparam>
        /// <param name="callback">The callback to unsubscribe.</param>
        /// <returns><see langword="true"/> if the callback was successfully unsubscribed; <see langword="false"/> otherwise.</returns>
        public bool TryUnsubscribeFromEvent<TEvent>(Action<EventArgs> callback) where TEvent : Event, new()
        {
            if (Events.TryGetValue(typeof(TEvent), out var @event) && @event is Event e)
            {
                e.TryRemoveCallback(callback);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to subscribe <paramref name="callback"/> from <typeparamref name="TAsyncEvent"/>.
        /// </summary>
        /// <typeparam name="TAsyncEvent">The type of the asynchronous event to subscribe <paramref name="callback"/> from.</typeparam>
        /// <param name="callback">The asynchronous callback to subscribe.</param>
        /// <returns><see langword="true"/> if the asynchronous callback was successfully subscribed; <see langword="false"/> otherwise.</returns>
        public bool TrySubscribeToAsyncEvent<TAsyncEvent>(Func<EventArgs, Task> callback) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                ae.AddCallback(callback);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to unsubscribe <paramref name="callback"/> from <typeparamref name="TAsyncEvent"/>.
        /// </summary>
        /// <typeparam name="TAsyncEvent">The type of the asynchronous event to unsubscribe <paramref name="callback"/> from.</typeparam>
        /// <param name="callback">The asynchronous callback to unsubscribe.</param>
        /// <returns><see langword="true"/> if the asynchronous callback was successfully unsubscribed; <see langword="false"/> otherwise.</returns>
        public bool TryUnubscribeFromAsyncEvent<TAsyncEvent>(Func<EventArgs, Task> callback) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                ae.TryRemoveCallback(callback);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to raise <typeparamref name="TEvent"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event to raise.</typeparam>
        /// <param name="eventArgs">Arguments to pass into event callbacks that are subscribed to <typeparamref name="TEvent"/>.</param>
        /// <returns><see langword="true"/> if <typeparamref name="TEvent"/> was successfullly raised; <see langword="false"/> otherwise.</returns>
        public bool TryRaiseEvent<TEvent>(EventArgs eventArgs) where TEvent : Event, new()
        {
            if (Events.TryGetValue(typeof(TEvent), out var @event) && @event is Event e)
            {
                e.Raise(eventArgs);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to raise <typeparamref name="TAsyncEvent"/> by calling all of its callbacks at once and awaits them all
        /// with <see cref="Task.WhenAll(IEnumerable{Task})"/>.
        /// </summary>
        /// <typeparam name="TAsyncEvent">The type of the asynchronous event to raise.</typeparam>
        /// <param name="eventArgs">Arguments to pass into event callbacks that are subscribed to <typeparamref name="TAsyncEvent"/>.</param>
        /// <returns>A <see cref="Task"/><![CDATA[<]]><see langword="bool"/><![CDATA[>]]> representing the asynchronous operation. The
        /// <see cref="Task"/><![CDATA[<]]><see langword="bool"/><![CDATA[>]]> returns <see langword="true"/> if <typeparamref name="TAsyncEvent"/>
        /// was successfullly raised; <see langword="false"/> otherwise.</returns>
        public async Task<bool> TryRaiseAsyncEventParallelAsync<TAsyncEvent>(EventArgs eventArgs) where TAsyncEvent : AsyncEvent, new()
        {
            if (Events.TryGetValue(typeof(TAsyncEvent), out var @event) && @event is AsyncEvent ae)
            {
                await ae.RaiseParallelAsync(eventArgs);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to raise <typeparamref name="TAsyncEvent"/> by calling and awaiting each of its callbacks one by one.
        /// </summary>
        /// <typeparam name="TAsyncEvent">The type of the asynchronous event to raise.</typeparam>
        /// <param name="eventArgs">Arguments to pass into event callbacks that are subscribed to <typeparamref name="TAsyncEvent"/>.</param>
        /// <returns>A <see cref="Task"/><![CDATA[<]]><see langword="bool"/><![CDATA[>]]> representing the asynchronous operation. The
        /// <see cref="Task"/><![CDATA[<]]><see langword="bool"/><![CDATA[>]]> returns <see langword="true"/> if <typeparamref name="TAsyncEvent"/>
        /// was successfullly raised; <see langword="false"/> otherwise.</returns>
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
