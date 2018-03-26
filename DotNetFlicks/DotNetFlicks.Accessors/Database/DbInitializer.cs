using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.Accessors.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        private static void SeedDepartments(DotNetFlicksContext context)
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

        private static void SeedMovies(DotNetFlicksContext context)
        {
            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Name = "The Dark Knight Rises",
                        Description = "Following the death of District Attorney Harvey Dent, Batman assumes responsibility for Dent's crimes to protect the late attorney's reputation and is subsequently hunted by the Gotham City Police Department. Eight years later, Batman encounters the mysterious Selina Kyle and the villainous Bane, a new terrorist leader who overwhelms Gotham's finest. The Dark Knight resurfaces to protect a city that has branded him an enemy.",
                        ReleaseDate = new DateTime(2012, 7, 20),
                        Runtime = new TimeSpan(0, 2, 45, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/dEYnvnUfXrqvqeRSqvIEtmzhoA8.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/g8evyE9TuYk",
                        RentCost = 3.99m,
                        PurchaseCost = 11.99m
                    },
                    new Movie
                    {
                        Name = "The Dark Knight Rises",
                        Description = "Following the death of District Attorney Harvey Dent, Batman assumes responsibility for Dent's crimes to protect the late attorney's reputation and is subsequently hunted by the Gotham City Police Department. Eight years later, Batman encounters the mysterious Selina Kyle and the villainous Bane, a new terrorist leader who overwhelms Gotham's finest. The Dark Knight resurfaces to protect a city that has branded him an enemy.",
                        ReleaseDate = new DateTime(2012, 7, 20),
                        Runtime = new TimeSpan(0, 2, 45, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/dEYnvnUfXrqvqeRSqvIEtmzhoA8.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/g8evyE9TuYk",
                        RentCost = 3.99m,
                        PurchaseCost = 11.99m
                    }
                };

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
