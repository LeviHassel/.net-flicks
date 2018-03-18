using CoreTemplate.Accessors.Identity;
using CoreTemplate.Accessors.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Accessors.Database
{
    public class CoreTemplateSeed
    {
        public static void Initialize(CoreTemplateContext context, UserManager<ApplicationUser> userManager)
        {
            //Create database if it doesn't exist and apply any pending migrations
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                var defaultUser = new ApplicationUser { UserName = "admin@coretemplate.com", Email = "admin@coretemplate.com" };
                userManager.CreateAsync(defaultUser, "Pass@word1");
            }

            if (!context.Genres.Any())
            {
                context.Genres.AddRange(GetDefaultGenres());
                context.SaveChanges();
            }

            if (!context.Departments.Any())
            {
                context.Departments.AddRange(GetDefaultDepartments());
                context.SaveChanges();
            }
        }

        #region Private Methods
        static List<Genre> GetDefaultGenres()
        {
            return new List<Genre>()
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
                new Genre { Name = "Sci-Fi" },
                new Genre { Name = "Thriller" },
                new Genre { Name = "War" },
                new Genre { Name = "Western" }
            };
        }

        static List<Department> GetDefaultDepartments()
        {
            return new List<Department>()
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
        }
        #endregion
    }
}