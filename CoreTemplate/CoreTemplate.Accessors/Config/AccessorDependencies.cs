using CoreTemplate.Accessors.Accessors;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreTemplate.Accessors.Config
{
    public static class AccessorDependencies
    {
        public static IServiceCollection AddAccessorDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<DbContext, CoreTemplateContext>();

            services.AddTransient<IGenreAccessor, GenreAccessor>();
            services.AddTransient<IJobAccessor, JobAccessor>();
            services.AddTransient<IMovieAccessor, MovieAccessor>();
            services.AddTransient<IPersonAccessor, PersonAccessor>();

            return services;
        }
    }
}
