using CoreTemplate.Accessors.Models.EF;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Accessors.Database
{
    public class CoreTemplateContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            var context = (CoreTemplateContext)applicationBuilder
                .ApplicationServices.GetService(typeof(CoreTemplateContext));

            // TODO: Only run this if using a real database (add if statement to determine env)
            // context.Database.Migrate();

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                    GetPreconfiguredMovies());

                await context.SaveChangesAsync();
            }
        }

        static IEnumerable<Movie> GetPreconfiguredMovies()
        {
            return new List<Movie>()
            {
                new Movie()
                {
                    Name = "Inception",
                    Director = "Christopher Nolan",
                    Genre = "Action",
                    Year = 2010,
                    Runtime = 148
                },
                new Movie()
                {
                    Name = "Airplane!",
                    Director = "David Zucker",
                    Genre = "Comedy",
                    Year = 1980,
                    Runtime = 88
                },
                new Movie()
                {
                    Name = "Toy Story",
                    Director = "John Lasseter",
                    Genre = "Animation",
                    Year = 1995,
                    Runtime = 81
                }
            };
        }
    }
}
