namespace SE7.Events.Services.Interfaces
{
    public interface IEventsService
    {
        Task<bool> TryRaiseAsyncEventParallelAsync<TAsyncEvent>(EventArgs eventArgs) where TAsyncEvent : AsyncEvent, new();
        Task<bool> TryRaiseAsyncEventSequentiallyAsync<TAsyncEvent>(EventArgs eventArgs) where TAsyncEvent : AsyncEvent, new();
        bool TryRaiseEvent<TEvent>(EventArgs eventArgs) where TEvent : Event, new();
        bool TryRegisterAsyncEvent<TAsyncEvent>() where TAsyncEvent : AsyncEvent, new();
        bool TryRegisterEvent<TEvent>() where TEvent : Event, new();
        bool TrySubscribeToAsyncEvent<TAsyncEvent>(Func<EventArgs, Task> callback) where TAsyncEvent : AsyncEvent, new();
        bool TrySubscribeToEvent<TEvent>(Action<EventArgs> callback) where TEvent : Event, new();
        bool TryUnsubscribeFromEvent<TEvent>(Action<EventArgs> callback) where TEvent : Event, new();
        bool TryUnubscribeFromAsyncEvent<TAsyncEvent>(Func<EventArgs, Task> callback) where TAsyncEvent : AsyncEvent, new();
    }
}