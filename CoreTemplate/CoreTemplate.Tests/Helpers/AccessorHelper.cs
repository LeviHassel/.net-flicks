using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Models.EF;
using CoreTemplate.Tests.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CoreTemplate.Tests.Helpers
{
    public class AccessorHelper : IDisposable
    {
        public CoreTemplateContext Context { get; private set; }

        private readonly TestServer _server;

        private readonly HttpClient _client;

        public AccessorHelper()
        {
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>();

            _server = new TestServer(builder);

            _client = _server.CreateClient();

            CreateTestDatabase();
        }

        public void CreateTestDatabase()
        {
            //For more information about testing with SQLite, go here: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CoreTemplateContext>()
                .UseSqlite(connection)
                .Options;

            Context = new CoreTemplateContext(options) { };
            Context.Database.OpenConnection();
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Database.CloseConnection();
            _server.Dispose();
            _client.Dispose();
        }

        /*
         * TODO: Revise SeedMovies to look less like the original code and be more efficient
         */
        internal List<Movie> SeedMovies(int count = 1)
        {
            //Set up Fixture to populate random data
            Fixture fixture = new Fixture();

            var entities = new List<Movie>(count);

            for (int i = 0; i < count; i++)
            {
                entities.Add(fixture.Create<Movie>());
            }

            Context.Movies.AddRange(entities);
            Context.SaveChanges();

            //Detach created entities so that tests don't run into EF conflicts
            foreach (var e in entities)
            {
                Context.Entry(e).State = EntityState.Detached;
            }

            return entities;
        }
    }
}
