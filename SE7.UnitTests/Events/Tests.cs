using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SE7.Events;
using SE7.Events.Args;
using SE7.Events.Extensions;
using SE7.Events.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.UnitTests.Events
{
    internal class Tests
    {
        public const string TestValue = "Potato";

        private class Event1 : Event;
        private class AsyncEvent1 : AsyncEvent;
        private class EventArgs1 : EventArgs
        {
            public string Value { get; }

            public EventArgs1(string value) => Value = value;
        }

        private class Service1
        {
            private readonly IEventsService EventsService;

            public string? Value1 { get; private set; }
            public Task<string>? Value2Task { get; private set; }

            public Service1(IEventsService eventsService)
            {
                EventsService = eventsService;

                eventsService.TryRegisterEvent<Event1>();
                eventsService.TryRegisterAsyncEvent<AsyncEvent1>();
                eventsService.TrySubscribeToEvent<Event1>(e => Value1 = (e as EventArgs1)?.Value);
                eventsService.TrySubscribeToAsyncEvent<AsyncEvent1>(e => Value2Task = Task.FromResult(TestValue));
            }
        }

        private class Service2
        {
            private readonly IEventsService EventsService;

            public string? Value { get; private set; }

            public Service2(IEventsService eventsService)
            {
                EventsService = eventsService;
            }

            public bool TryRaiseEvent1() => EventsService.TryRaiseEvent<Event1>(new EventArgs1(TestValue));
            public async Task<bool> TryRaiseAsyncEvent1() => await EventsService.TryRaiseAsyncEventParallelAsync<AsyncEvent1>(EventArgs.Empty);
        }

        [Test(Author = "SE7", Description = "Test That EventsService Raises Events Between Services")]
        public async Task TestThatEventsServiceRaisesEventsBetweenServices()
        {
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddEvents();
                    services.AddSingleton<Service1>();
                    services.AddSingleton<Service2>();
                })
                .Build()
            ;

            var service1 = host.Services.GetRequiredService<Service1>();
            var service2 = host.Services.GetRequiredService<Service2>();

            service2.TryRaiseEvent1();
            await service2.TryRaiseAsyncEvent1();

            Assert.That(service1.Value2Task, Is.Not.Null);
            Assert.That(await service1.Value2Task!, Is.EqualTo(TestValue));
            Assert.That(service1.Value1, Is.EqualTo(TestValue));
        }
    }
}
