using CoreTemplate.Accessors;
using CoreTemplate.Accessors.Accessors;
using CoreTemplate.Accessors.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

/*
 * TODO: Address comments based on what problems not having them causes
 */

namespace CoreTemplate.Web.Config
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
            services.AddTransient<IMovieAccessor, MovieAccessor>();

            //Currently unused bindings from Ninject:
            //Kernel.Bind<CoreTemplateContext>().ToSelf().InRequestScope();
            //Kernel.Bind<IUserStore<ApplicationUser, string>>().To<ApplicationUserStore>();

            return services;
        }
    }
}
