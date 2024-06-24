using Microsoft.Extensions.DependencyInjection;
using SE7.Threading.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
