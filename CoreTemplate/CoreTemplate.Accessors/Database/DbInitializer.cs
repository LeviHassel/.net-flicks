using CoreTemplate.Accessors.Identity;
using CoreTemplate.Accessors.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Accessors.Database
{
    public class DbInitializer
    {
        public static void Initialize(CoreTemplateContext context, UserManager<ApplicationUser> userManager)
        {
            //Create database if it doesn't exist and apply any pending migrations
            context.Database.Migrate();

            //Seed database
            SeedAdmin(context, userManager);
            SeedGenres(context);
            SeedDepartments(context);
        }

        #region Private Methods
        private static void SeedAdmin(CoreTemplateContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@coretemplate.com",
                    Email = "admin@coretemplate.com",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(admin, "p@ssWORD471");
            }
        }

        private static void SeedGenres(CoreTemplateContext context)
        {
            if (!context.Genres.Any())
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Action" },
                    new Genre { Name = "Adventure" },
                    new Genre { Name = "Animation" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Crime" },
                    new Genre { Name = "Documentary" },
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Family" },
                    new Genre { Name = "Fantasy" },
                    new Genre { Name = "History" },
                    new Genre { Name = "Horror" },
                    new Genre { Name = "Music" },
                    new Genre { Name = "Mystery" },
                    new Genre { Name = "Romance" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Thriller" },
                    new Genre { Name = "War" },
                    new Genre { Name = "Western" }
                };

                context.Genres.AddRange(genres);
                context.SaveChanges();
            }
        }

        private static void SeedDepartments(CoreTemplateContext context)
        {
            if (!context.Departments.Any())
            {
                var departments = new List<Department>()
                {
                    new Department { Name = "Art" },
                    new Department { Name = "Camera" },
                    new Department { Name = "Costume & Make-Up" },
                    new Department { Name = "Crew" },
                    new Department { Name = "Directing", IsDirecting = true },
                    new Department { Name = "Editing" },
                    new Department { Name = "Lighting" },
                    new Department { Name = "Production" },
                    new Department { Name = "Sound" },
                    new Department { Name = "Visual Effects" },
                    new Department { Name = "Writing" }
                };

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
