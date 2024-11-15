# SE7.Events
## A library that add events to a Dependency Injection container.

This is a simple library that allows for you to raise and consume events in between services that are in a Dependency Injection container.
This package is currently only compatible with Microsoft.Extensions.DependencyInjection, but can be expanded in the future.

## Usage

# Adding the service

```csharp
using SE7.Events.Extensions;

// Add events to container
host
	.ConfigureServices((_, services) =>
	{
		// ...
		
		services.AddEvents(); // Will inject EventsService
	})

```

# Creating an Event

```csharp

// Callbacks will be of type Action<EventArgs>
public class MyEvent : Event;

// Async version available
// Callbacks will be of type Func<EventArgs, Task>
public class MyAsyncEvent : AsyncEvent;

// You can also create your own custom event with your own
// custom type of callback. Do this by implementing IEvent<TDelegate> instead.
// The below example uses a Action<int, char, string>:
public class MyCustomEvent : IEvent<Action<int, char, string>>
{
	// Implement public void AddCallback(TDelegate @delegate) and public bool TryRemoveCallback(TDelegate @delegate) here...
}

```

# Registering + Subscribing to an event
Given that you have added `EventsService` using the AddEvents extension method, you can inject it into any of your services,
keeping in mind that it is a singleton service.

```csharp

class MyService1
{
	private readonly EventsService EventsService;

	public MyService1(EventsService eventsService)
	{
		EventsService = eventsService;

		EventsService.TryRegisterEvent<MyEvent>();
		EventsService.TrySubscribeToEvent<MyEvent>(e =>
		{
			// Event Callback here...
		});
	}
}

class MyService2
{
	private readonly EventsService EventsService;

	public MyService2(EventsService eventsService)
	{
		EventsService = eventsService;
	}

	public void Foo()
	{
		EventsService.TryRaiseEvent<MyEvent>(EventArgs.Empty);

		// ...
	}
}

```