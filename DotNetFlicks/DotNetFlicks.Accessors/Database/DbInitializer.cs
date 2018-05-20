using DotNetFlicks.Accessors.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetFlicks.Accessors.Database
{
    public class DbInitializer
    {
        public static void Initialize(DotNetFlicksContext context, UserManager<ApplicationUser> userManager)
        {
            //Create database if it doesn't exist and apply any pending migrations
            context.Database.Migrate();

            //Seed database
            SeedAdmin(context, userManager);
        }

        #region Private Methods
        private static void SeedAdmin(DotNetFlicksContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@dotnetflicks.com",
                    Email = "admin@dotnetflicks.com",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(admin, "p@ssWORD471");
            }
        }
        #endregion
    }
}
