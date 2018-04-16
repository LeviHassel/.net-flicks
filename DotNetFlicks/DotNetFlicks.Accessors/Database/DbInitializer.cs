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
            SeedMovieGenres(context);
            SeedCastMembers(context);
            SeedCrewMembers(context);
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

        private static void SeedMovieGenres(DotNetFlicksContext context)
        {
            if (!context.MovieGenres.Any())
            {
                var movieGenres = JsonConvert.DeserializeObject<List<MovieGenre>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "movieGenres.json"));

                //Because EF Core doesn't support seeding preset IDs, I've used the TmdbId column to connect the related seeding data
                foreach (var movieGenre in movieGenres)
                {
                    movieGenre.GenreId = context.Genres.Single(x => x.TmdbId == movieGenre.GenreId).Id;
                    movieGenre.MovieId = context.Movies.Single(x => x.TmdbId == movieGenre.MovieId).Id;
                }

                context.MovieGenres.AddRange(movieGenres);
                context.SaveChanges();
            }
        }

        private static void SeedCastMembers(DotNetFlicksContext context)
        {
            if (!context.CastMembers.Any())
            {
                var castMembers = JsonConvert.DeserializeObject<List<CastMember>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "castMembers.json"));

                //Because EF Core doesn't support seeding preset IDs, I've used the TmdbId column to connect the related seeding data
                foreach (var castMember in castMembers)
                {
                    castMember.MovieId = context.Movies.Single(x => x.TmdbId == castMember.MovieId).Id;
                    castMember.PersonId = context.People.Single(x => x.TmdbId == castMember.PersonId).Id;
                }

                context.CastMembers.AddRange(castMembers);
                context.SaveChanges();
            }
        }

        private static void SeedCrewMembers(DotNetFlicksContext context)
        {
            if (!context.CrewMembers.Any())
            {
                var crewMembers = JsonConvert.DeserializeObject<List<CrewMember>>(File.ReadAllText(@"Config" + Path.DirectorySeparatorChar + "SeedData" + Path.DirectorySeparatorChar + "crewMembers.json"));

                //Because EF Core doesn't support seeding preset IDs, I've used the TmdbId column to connect the related seeding data
                foreach (var crewMember in crewMembers)
                {
                    crewMember.MovieId = context.Movies.Single(x => x.TmdbId == crewMember.MovieId).Id;
                    crewMember.PersonId = context.People.Single(x => x.TmdbId == crewMember.PersonId).Id;
                    crewMember.DepartmentId = context.Departments.Single(x => x.TmdbId == crewMember.DepartmentId).Id;
                }

                context.CrewMembers.AddRange(crewMembers);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
