using DotNetFlicks.Accessors.Accessors;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetFlicks.Accessors.Config
{
    public static class AccessorDependencies
    {
        public static IServiceCollection AddAccessorDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<DbContext, DotNetFlicksContext>();

            services.AddTransient<ICastMemberAccessor, CastMemberAccessor>();
            services.AddTransient<ICrewMemberAccessor, CrewMemberAccessor>();
            services.AddTransient<IGenreAccessor, GenreAccessor>();
            services.AddTransient<IDepartmentAccessor, DepartmentAccessor>();
            services.AddTransient<IMovieAccessor, MovieAccessor>();
            services.AddTransient<IMovieGenreAccessor, MovieGenreAccessor>();
            services.AddTransient<IPersonAccessor, PersonAccessor>();
            services.AddTransient<IUserMovieAccessor, UserMovieAccessor>();

            return services;
        }
    }
}
