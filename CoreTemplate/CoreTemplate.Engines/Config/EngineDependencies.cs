using CoreTemplate.Engines.Engines;
using CoreTemplate.Engines.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreTemplate.Engines.Config
{
    public static class EngineDependencies
    {
        public static IServiceCollection AddEngineDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<IPersonEngine, PersonEngine>();

            return services;
        }
    }
}
