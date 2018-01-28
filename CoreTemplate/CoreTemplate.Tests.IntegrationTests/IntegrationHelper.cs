using CoreTemplate.Accessors.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;

namespace CoreTemplate.Tests.IntegrationTests
{
    public class IntegrationHelper : IDisposable
    {
        /* Currently this does nothing- it's only a template becuase I know I'll need a client and server */

        public CoreTemplateContext Context { get; private set; }

        private readonly TestServer _server;

        private readonly HttpClient _client;

        public IntegrationHelper()
        {
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>();

            //Server and client are needed for integration testing
            //https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing
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
    }
}
