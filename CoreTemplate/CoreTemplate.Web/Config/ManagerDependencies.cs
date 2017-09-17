using CoreTemplate.Managers.Interfaces;
using CoreTemplate.Managers.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;

/*
 * TODO: Address comments based on what problems not having them causes
 */

namespace CoreTemplate.Web.Config
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
            Ninject Dependencies

            //identity binds
            Kernel.Bind<IIdentityService>().To<IdentityService>();
            Kernel.Bind<ApplicationUserManager>().ToSelf();
            Kernel.Bind<ApplicationSignInManager>().ToSelf();
            Kernel.Bind<IdentityFactoryOptions<ApplicationUserManager>>().ToSelf();
            Kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope();

            //services
            Kernel.Bind<IAccountService>().To<AccountService>();
            */

            return services;
        }
    }
}
