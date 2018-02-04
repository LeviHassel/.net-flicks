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

            //services.AddTransient<ITestEngine, TestEngine>();

            return services;
        }
    }
}
