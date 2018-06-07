using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DotNetFlicks.Web.Config
{
    public static class DbInitializer
    {
        public async static void Run(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DotNetFlicksContext>();

                    //Create database if it doesn't exist and apply any pending migrations (this will also seed data)
                    context.Database.Migrate();

                    //Seed admin user
                    if (!context.Users.Any())
                    {
                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                        var admin = new ApplicationUser
                        {
                            UserName = "admin@dotnetflicks.com",
                            Email = "admin@dotnetflicks.com",
                            EmailConfirmed = true
                        };

                        await userManager.CreateAsync(admin, "p@ssWORD471");
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}