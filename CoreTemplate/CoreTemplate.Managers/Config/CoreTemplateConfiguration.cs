using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreTemplate.Managers.Config
{
    public static class CoreTemplateConfiguration
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<CoreTemplateContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CoreTemplateContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Set up dependency injection
            services.AddManagerDependencies();
            services.AddAccessorDependencies();

            return services;
        }
    }
}
