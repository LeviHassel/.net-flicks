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
            SeedMovieGenres(context);
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
                        Name = "Birdman",
                        Description = "A fading actor best known for his portrayal of a popular superhero attempts to mount a comeback by appearing in a Broadway play. As opening night approaches, his attempts to become more altruistic, rebuild his career, and reconnect with friends and family prove more difficult than expected.",
                        ReleaseDate = new DateTime(2014, 10, 17),
                        Runtime = new TimeSpan(0, 1, 59, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/g3UsWc4GdTGav2bRiYfwA4reulH.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/xIxMMv_LD5Q",
                        RentCost = 3.99m,
                        PurchaseCost = 11.99m
                        //Genres: Drama, Comedy
                    },
                    new Movie
                    {
                        Name = "Coco",
                        Description = "Despite his family’s baffling generations-old ban on music, Miguel dreams of becoming an accomplished musician like his idol, Ernesto de la Cruz. Desperate to prove his talent, Miguel finds himself in the stunning and colorful Land of the Dead following a mysterious chain of events. Along the way, he meets charming trickster Hector, and together, they set off on an extraordinary journey to unlock the real story behind Miguel's family history.",
                        ReleaseDate = new DateTime(2017, 11, 22),
                        Runtime = new TimeSpan(0, 1, 45, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/eKi8dIrr8voobbaGzDpe8w0PVbC.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/xlnPHQ3TLX8",
                        RentCost = 4.99m,
                        PurchaseCost = 14.99m
                        //Genres: Adventure, Animation, Comedy, Family
                    },
                    new Movie
                    {
                        Name = "The Dark Knight Rises",
                        Description = "Following the death of District Attorney Harvey Dent, Batman assumes responsibility for Dent's crimes to protect the late attorney's reputation and is subsequently hunted by the Gotham City Police Department. Eight years later, Batman encounters the mysterious Selina Kyle and the villainous Bane, a new terrorist leader who overwhelms Gotham's finest. The Dark Knight resurfaces to protect a city that has branded him an enemy.",
                        ReleaseDate = new DateTime(2012, 7, 20),
                        Runtime = new TimeSpan(0, 2, 45, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/dEYnvnUfXrqvqeRSqvIEtmzhoA8.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/g8evyE9TuYk",
                        RentCost = 2.99m,
                        PurchaseCost = 9.99m
                        //Genres: Action, Crime, Drama, Thriller
                    },
                    new Movie
                    {
                        Name = "Django Unchained",
                        Description = "With the help of a German bounty hunter, a freed slave sets out to rescue his wife from a brutal Mississippi plantation owner.",
                        ReleaseDate = new DateTime(2012, 12, 25),
                        Runtime = new TimeSpan(0, 2, 45, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/5WJnxuw41sddupf8cwOxYftuvJG.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/_iH0UBYDI4g",
                        RentCost = 2.99m,
                        PurchaseCost = 9.99m
                        //Genres: Drama, Western
                    },
                    new Movie
                    {
                        Name = "Ferris Bueller's Day Off",
                        Description = "Charismatic teen Ferris Bueller plays hooky in Chicago with his girlfriend and best friend.",
                        ReleaseDate = new DateTime(1986, 6, 11),
                        Runtime = new TimeSpan(0, 1, 43, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/kto49vDiSzooEdy4WQH2RtaC9oP.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/D6gABQFR94U",
                        RentCost = 1.99m,
                        PurchaseCost = 7.99m
                        //Genres: Comedy
                    },
                    new Movie
                    {
                        Name = "Get Out",
                        Description = "Chris and his girlfriend Rose go upstate to visit her parents for the weekend. At first, Chris reads the family's overly accommodating behavior as nervous attempts to deal with their daughter's interracial relationship, but as the weekend progresses, a series of increasingly disturbing discoveries lead him to a truth that he never could have imagined.",
                        ReleaseDate = new DateTime(2017, 2, 24),
                        Runtime = new TimeSpan(0, 1, 44, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/1SwAVYpuLj8KsHxllTF8Dt9dSSX.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/sRfnevzM9kQ",
                        RentCost = 4.99m,
                        PurchaseCost = 12.99m
                        //Genres: Mystery, Thriller, Horror
                    },
                    new Movie
                    {
                        Name = "Inception",
                        Description = "Cobb, a skilled thief who commits corporate espionage by infiltrating the subconscious of his targets is offered a chance to regain his old life as payment for a task considered to be impossible: \"inception\", the implantation of another person's idea into a target's subconscious.",
                        ReleaseDate = new DateTime(2010, 7, 16),
                        Runtime = new TimeSpan(0, 2, 28, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/qmDpIHrmpJINaRKAfWQfftjCdyi.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/xitHF0IPJSQ",
                        RentCost = 2.99m,
                        PurchaseCost = 10.99m
                        //Genres: Adventure, Thriller, Action, Mystery, Science Fiction
                    },
                    new Movie
                    {
                        Name = "Interstellar",
                        Description = "Interstellar chronicles the adventures of a group of explorers who make use of a newly discovered wormhole to surpass the limitations on human space travel and conquer the vast distances involved in an interstellar voyage.",
                        ReleaseDate = new DateTime(2014, 11, 5),
                        Runtime = new TimeSpan(0, 2, 49, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/nBNZadXqJSdt05SHLqgT0HuC5Gm.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/ePbKGoIGAXY",
                        RentCost = 3.99m,
                        PurchaseCost = 11.99m
                        //Genres: Adventure, Drama, Science Fiction
                    },
                    new Movie
                    {
                        Name = "The Lion King",
                        Description = "A young lion cub named Simba can't wait to be king. But his uncle craves the title for himself and will stop at nothing to get it.",
                        ReleaseDate = new DateTime(1994, 6, 23),
                        Runtime = new TimeSpan(0, 1, 29, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/bKPtXn9n4M4s8vvZrbw40mYsefB.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/4sj1MT05lAA",
                        RentCost = 2.99m,
                        PurchaseCost = 9.99m
                        //Genres: Family, Animation, Drama
                    },
                    new Movie
                    {
                        Name = "The Lord of the Rings: The Return of the King",
                        Description = "Aragorn is revealed as the heir to the ancient kings as he, Gandalf and the other members of the broken fellowship struggle to save Gondor from Sauron's forces. Meanwhile, Frodo and Sam bring the ring closer to the heart of Mordor, the dark lord's realm.",
                        ReleaseDate = new DateTime(2003, 12, 17),
                        Runtime = new TimeSpan(0, 3, 21, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/rCzpDGLbOoPwLjy3OAm5NUPOTrC.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/co9SNfJNDN8",
                        RentCost = 1.99m,
                        PurchaseCost = 7.99m
                        //Genres: Adventure, Fantasy, Action
                    },
                    new Movie
                    {
                        Name = "Pulp Fiction",
                        Description = "A burger-loving hit man, his philosophical partner, a drug-addled gangster's moll and a washed-up boxer converge in this sprawling, comedic crime caper. Their adventures unfurl in three stories that ingeniously trip back and forth in time.",
                        ReleaseDate = new DateTime(1994, 10, 14),
                        Runtime = new TimeSpan(0, 2, 34, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/dM2w364MScsjFf8pfMbaWUcWrR.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/s7EdQ4FqbhY",
                        RentCost = 2.99m,
                        PurchaseCost = 9.99m
                        //Genres: Thriller, Crime
                    },
                    new Movie
                    {
                        Name = "The Revenant",
                        Description = "In the 1820s, a frontiersman, Hugh Glass, sets out on a path of vengeance against those who left him for dead after a bear mauling.",
                        ReleaseDate = new DateTime(2016, 1, 8),
                        Runtime = new TimeSpan(0, 2, 36, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/oXUWEc5i3wYyFnL1Ycu8ppxxPvs.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/QRfj1VCg16Y",
                        RentCost = 3.99m,
                        PurchaseCost = 11.99m
                        //Genres: Adventure, Drama, Thriller, Western
                    },
                    new Movie
                    {
                        Name = "The Ritual",
                        Description = "A group of college friends reunite for a trip to the forest, but encounter a menacing presence in the woods that's stalking them.",
                        ReleaseDate = new DateTime(2018, 2, 9),
                        Runtime = new TimeSpan(0, 1, 34, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/hHuJqzby593lmYmw1SzT0XYy99t.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/aBFyixPE7zA",
                        RentCost = 4.99m,
                        PurchaseCost = 13.99m
                        //Genres: Horror
                    },
                    new Movie
                    {
                        Name = "Saving Private Ryan",
                        Description = "As U.S. troops storm the beaches of Normandy, three brothers lie dead on the battlefield, with a fourth trapped behind enemy lines. Ranger captain John Miller and seven men are tasked with penetrating German-held territory and bringing the boy home.",
                        ReleaseDate = new DateTime(1998, 7, 24),
                        Runtime = new TimeSpan(0, 2, 49, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/miDoEMlYDJhOCvxlzI0wZqBs9Yt.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/RYExstiQlLc",
                        RentCost = 1.99m,
                        PurchaseCost = 7.99m
                        //Genres: Drama, History, War
                    },
                    new Movie
                    {
                        Name = "Schindler's List",
                        Description = "The true story of how businessman Oskar Schindler saved over a thousand Jewish lives from the Nazis while they worked as slaves in his factory during World War II.",
                        ReleaseDate = new DateTime(1993, 11, 29),
                        Runtime = new TimeSpan(0, 3, 15, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/yPisjyLweCl1tbgwgtzBCNCBle.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/bJcLRFWxRno",
                        RentCost = 2.99m,
                        PurchaseCost = 9.99m
                        //Genres: Drama, History, War
                    },
                    new Movie
                    {
                        Name = "The Shape of Water",
                        Description = "An other-worldly story, set against the backdrop of Cold War era America circa 1962, where a mute janitor working at a lab falls in love with an amphibious man being held captive there and devises a plan to help him escape.",
                        ReleaseDate = new DateTime(2017, 12, 22),
                        Runtime = new TimeSpan(0, 2, 3, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/k4FwHlMhuRR5BISY2Gm2QZHlH5Q.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/XFYWazblaUA",
                        RentCost = 4.99m,
                        PurchaseCost = 12.99m
                        //Genres: Drama, Fantasy, Romance
                    },
                    new Movie
                    {
                        Name = "Star Wars: Episode V - The Empire Strikes Back",
                        Description = "The epic saga continues as Luke Skywalker, in hopes of defeating the evil Galactic Empire, learns the ways of the Jedi from aging master Yoda. But Darth Vader is more determined than ever to capture Luke. Meanwhile, rebel leader Princess Leia, cocky Han Solo, Chewbacca, and droids C-3PO and R2-D2 are thrown into various stages of capture, betrayal and despair.",
                        ReleaseDate = new DateTime(1980, 6, 20),
                        Runtime = new TimeSpan(0, 2, 4, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/9SKDSFbaM6LuGqG1aPWN3wYGEyD.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/KwYa7UpoWtM",
                        RentCost = 2.99m,
                        PurchaseCost = 5.99m
                        //Genres: Adventure, Action, Science Fiction
                    },
                    new Movie
                    {
                        Name = "Straight Outta Compton",
                        Description = "In 1987, five young men, using brutally honest rhymes and hardcore beats, put their frustration and anger about life in the most dangerous place in America into the most powerful weapon they had: their music. Taking us back to where it all began, Straight Outta Compton tells the true story of how these cultural rebels—armed only with their lyrics, swagger, bravado and raw talent—stood up to the authorities that meant to keep them down and formed the world’s most dangerous group, N.W.A. And as they spoke the truth that no one had before and exposed life in the hood, their voice ignited a social revolution that is still reverberating today.",
                        ReleaseDate = new DateTime(2015, 8, 14),
                        Runtime = new TimeSpan(0, 2, 27, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/X7S1RtotXOZNV7OlgCfh5VKZSB.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/rsbWEF1Sju0",
                        RentCost = 3.99m,
                        PurchaseCost = 8.99m
                        //Genres: Drama, Music
                    },
                    new Movie
                    {
                        Name = "Toy Story 3",
                        Description = "Woody, Buzz, and the rest of Andy's toys haven't been played with in years. With Andy about to go to college, the gang find themselves accidentally left at a nefarious day care center. The toys must band together to escape and return home to Andy.",
                        ReleaseDate = new DateTime(2010, 6, 17),
                        Runtime = new TimeSpan(0, 1, 43, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/AqYmOBxLjASrj5UtybIh7Axyv77.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/TNMpa5yBf5o",
                        RentCost = 3.99m,
                        PurchaseCost = 11.99m
                        //Genres: Animation, Comedy, Family
                    },
                    new Movie
                    {
                        Name = "Tropic Thunder",
                        Description = "Vietnam veteran 'Four Leaf' Tayback's memoir, Tropic Thunder, is being made into a film, but Director Damien Cockburn can’t control the cast of prima donnas. Behind schedule and over budget, Cockburn is ordered by a studio executive to get filming back on track, or risk its cancellation. On Tayback's advice, Cockburn drops the actors into the middle of the jungle to film the remaining scenes but, unbeknownst to the actors and production, the group have been dropped in the middle of the Golden Triangle, the home of heroin-producing gangs.",
                        ReleaseDate = new DateTime(2008, 8, 13),
                        Runtime = new TimeSpan(0, 1, 47, 0, 0),
                        ImageUrl = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/zoWUdaaWKPDyr9b0il0YcggDWgJ.jpg",
                        TrailerUrl = "https://www.youtube.com/embed/VsEdmjAudSI",
                        RentCost = 2.99m,
                        PurchaseCost = 8.99m
                        //Genres: Action, Comedy
                    }
                };

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }

        private static void SeedMovieGenres(DotNetFlicksContext context)
        {
            if (!context.MovieGenres.Any())
            {
                var movieGenres = new List<MovieGenre>()
                {
                    new MovieGenre { }
                };

                context.MovieGenres.AddRange(movieGenres);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
