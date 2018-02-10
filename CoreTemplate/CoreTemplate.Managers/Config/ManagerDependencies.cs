using CoreTemplate.Managers.Identity;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreTemplate.Managers.Config
{
    public static class ManagerDependencies
    {
        public static IServiceCollection AddManagerDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<ApplicationUserManager>();
            services.AddTransient<ApplicationSignInManager>();

            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IEmailManager, EmailManager>();
            services.AddTransient<IGenreManager, GenreManager>();
            services.AddTransient<IJobManager, JobManager>();
            services.AddTransient<IMovieManager, MovieManager>();
            services.AddTransient<IPersonManager, PersonManager>();

            return services;
        }
    }
}
