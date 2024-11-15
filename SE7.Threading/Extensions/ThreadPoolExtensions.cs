using Microsoft.Extensions.DependencyInjection;
using SE7.Threading.Services.Implementations;

namespace SE7.Threading.Extensions
{
    public static class ThreadPoolExtensions
    {
        public static IServiceCollection AddThreadPools(this IServiceCollection services)
        {
            services.AddSingleton<ThreadPoolService>();

            return services;
        }
    }
}
