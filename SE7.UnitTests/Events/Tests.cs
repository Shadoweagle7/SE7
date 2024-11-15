using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SE7.Events;
using SE7.Events.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.UnitTests.Events
{
    internal class Tests
    {
        private class Event1 : Event;
        private class AsyncEvent1 : AsyncEvent;
        private class EventArgs1 : EventArgs
        {
            public string Value { get; }

            public EventArgs1(string value) => Value = value;
        }

        private class Service1
        {
            private readonly EventsService EventsService;

            public string? Value1 { get; private set; }
            public Task? Value2Task { get; private set; }

            public Service1(EventsService eventsService)
            {
                EventsService = eventsService;

                eventsService.TryRegisterEvent<Event1>();
                eventsService.TryRegisterAsyncEvent<AsyncEvent1>();
                eventsService.TrySubscribeEvent<Event1>(e => Value1 = (e as EventArgs1)?.Value);
                eventsService.TrySubscribeAsyncEvent<AsyncEvent1>(e => );
            }
        }

        private class Service2
        {
            public const string TestValue = "Potato";
            private readonly EventsService EventsService;

            public string? Value { get; private set; }

            public Service2(EventsService eventsService)
            {
                EventsService = eventsService;
            }

            public bool TryRaiseEvent1() => EventsService.TryRaiseEvent<Event1>(new EventArgs1(TestValue));
        }

        [Test(Author = "SE7", Description = "Test That EventsService Raises Events Between Services")]
        public void TestThatEventsServiceRaisesEventsBetweenServices()
        {
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton<EventsService>();
                    services.AddSingleton<Service1>();
                    services.AddSingleton<Service2>();
                })
                .Build()
            ;

            var service1 = host.Services.GetRequiredService<Service1>();
            var service2 = host.Services.GetRequiredService<Service2>();

            service2.TryRaiseEvent1();

            Assert.That(service1.Value1, Is.EqualTo(Service2.TestValue));
        }
    }
}
