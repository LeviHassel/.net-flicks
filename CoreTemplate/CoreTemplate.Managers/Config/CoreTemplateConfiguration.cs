using CoreTemplate.Accessors;
using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace CoreTemplate.Managers.Config
{
    public static class CoreTemplateConfiguration
    {
        public static IServiceCollection ConfigureCoreTemplate(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddDbContext<CoreTemplateContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //TODO: Add testing connection setup here (see the line above)

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CoreTemplateContext>()
                .AddDefaultTokenProviders();

            // Add application services
            services.AddManagerDependencies();
            services.AddAccessorDependencies();

            return services;
        }
    }
}
