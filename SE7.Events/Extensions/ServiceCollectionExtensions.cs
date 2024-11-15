using Microsoft.Extensions.DependencyInjection;

namespace SE7.Events.Extensions
{
    /// <summary>
    /// Represents a collection of extension methods to add events to a Dependency Injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds an <see cref="EventsService"/> as a singleton service to <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The collection of services to add an <see cref="EventsService"/> to.</param>
        /// <returns><paramref name="services"/> for method chaining.</returns>
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<EventsService>();

            return services;
        }
    }
}
