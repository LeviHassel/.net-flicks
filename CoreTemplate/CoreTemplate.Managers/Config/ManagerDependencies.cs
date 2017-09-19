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

            services.AddTransient<IEmailManager, EmailManager>();
            services.AddTransient<IMovieManager, MovieManager>();

            /*
            TODO: Port over needed Ninject dependencies (see if not having these causes any problems)

            //identity binds
            Kernel.Bind<ApplicationUserManager>().ToSelf();
            Kernel.Bind<ApplicationSignInManager>().ToSelf();
            Kernel.Bind<IdentityFactoryOptions<ApplicationUserManager>>().ToSelf();
            Kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope();
            */

            return services;
        }
    }
}
