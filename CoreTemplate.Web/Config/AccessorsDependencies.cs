using CoreTemplate.Accessors;
using CoreTemplate.Accessors.Accessors;
using CoreTemplate.Accessors.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

/*
 * TODO: Address comments based on what problems not having them causes - Levi Hassel
 */

namespace CoreTemplate.Web.Config
{
    public static class AccessorsDependencies
    {
        public static IServiceCollection AddAccessorsDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddTransient<IMovieAccessor, MovieAccessor>();

            //Currently unused bindings from  Ninject:
            //Kernel.Bind<MvcIndividualAuthContext>().ToSelf().InRequestScope();
            //Kernel.Bind<IUserStore<ApplicationUser, string>>().To<ApplicationUserStore>();

            return services;
        }
    }
}
