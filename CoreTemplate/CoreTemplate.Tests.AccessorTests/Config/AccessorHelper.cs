using AutoFixture;
using AutoMapper;
using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Models.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreTemplate.Tests.AccessorTests.Config
{
    public class AccessorHelper : IDisposable
    {
        public CoreTemplateContext Context { get; private set; }

        private Fixture _fixture;

        public AccessorHelper()
        {
            //Set up a SQLite in-memory connection: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CoreTemplateContext>()
                .UseSqlite(connection)
                .Options;

            Context = new CoreTemplateContext(options) { };
            Context.Database.OpenConnection();
            Context.Database.EnsureCreated();

            //Set up a Fixture to populate random data: https://github.com/AutoFixture/AutoFixture
            _fixture = new Fixture();

            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<AccessorMapper>();
            });
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
                .With(x => x.Genre, "something")
                .CreateMany(count)
                .ToList();

            Context.Movies.AddRange(movies);

            return UpdateDatabase(movies);
        }

        internal List<T> UpdateDatabase<T>(List<T> items)
        {
            Context.SaveChanges();

            //Because the context remains open between tests, every newly created entity needs to be detached
            foreach (var item in items)
            {
                Context.ChangeTracker.TrackGraph(item, x => x.Entry.State = EntityState.Detached);
            }

            return items;
        }
    }
}