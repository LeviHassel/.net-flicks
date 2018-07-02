using AutoFixture;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Models.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.Tests.AccessorTests.Config
{
    public class DatabaseHelper : IDisposable
    {
        public DotNetFlicksContext Context { get; private set; }

        private Fixture _fixture;

        public DatabaseHelper()
        {
            //Set up a SQLite in-memory connection: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DotNetFlicksContext>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .Options;

            Context = new DotNetFlicksContext(options) { };
            Context.Database.OpenConnection();
            Context.Database.EnsureCreated();

            //Set up a Fixture to populate random data: https://github.com/AutoFixture/AutoFixture
            _fixture = new Fixture();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Database.CloseConnection();
        }

        internal List<Movie> SeedMovies(int count = 1)
        {
            //TODO: Remove .With once I have an example of it elsewhere
            var movies = _fixture.Build<Movie>()
                .With(x => x.ImageUrl, "something.com")
                .CreateMany(count)
                .ToList();

            Context.Movies.AddRange(movies);
            Context.SaveChanges();
            Context.DetachAll();

            return movies;
        }
    }
}