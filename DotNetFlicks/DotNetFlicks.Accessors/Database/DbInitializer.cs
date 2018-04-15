using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.Accessors.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
            SeedGenres(context);
            SeedDepartments(context);
            SeedPeople(context);
            SeedMovies(context);
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

        private static void SeedGenres(DotNetFlicksContext context)
        {
            if (!context.Genres.Any())
            {
                var genres = JsonConvert.DeserializeObject<List<Genre>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "genres.json"));

                context.Genres.AddRange(genres);
                context.SaveChanges();
            }
        }

        private static void SeedDepartments(DotNetFlicksContext context)
        {
            if (!context.Departments.Any())
            {
                var departments = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "departments.json"));

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }
        }

        private static void SeedPeople(DotNetFlicksContext context)
        {
            if (!context.People.Any())
            {
                var people = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "people.json"));

                context.People.AddRange(people);
                context.SaveChanges();
            }
        }

        private static void SeedMovies(DotNetFlicksContext context)
        {
            if (!context.Movies.Any())
            {
                var movies = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "movies.json"));

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
