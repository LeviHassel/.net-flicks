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
            services.AddTransient<IMovieAccessor, MovieAccessor>();

            //TODO: Port over needed Ninject dependencies (see if not having these causes any problems):
            //Kernel.Bind<CoreTemplateContext>().ToSelf().InRequestScope();
            //Kernel.Bind<IUserStore<ApplicationUser, string>>().To<ApplicationUserStore>();

            return services;
        }
    }
}
