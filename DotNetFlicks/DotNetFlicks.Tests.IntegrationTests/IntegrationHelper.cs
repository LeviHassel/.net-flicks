using AutoFixture;
using AutoMapper;
using DotNetFlicks.Accessors.Config;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Managers.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;

namespace DotNetFlicks.Tests.IntegrationTests
{
    public class IntegrationHelper : IDisposable
    {
        public DotNetFlicksContext Context { get; private set; }

        private readonly TestServer _server;

        private readonly HttpClient _client;

        private Fixture _fixture;

        public IntegrationHelper()
        {
            //Configure server and client for integration testing: https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing
            var builder = new WebHostBuilder()
                .UseStartup<TestStartup>();

            _server = new TestServer(builder);

            _client = _server.CreateClient();

            CreateTestDatabase();

            //Set up a Fixture to populate random data: https://github.com/AutoFixture/AutoFixture
            _fixture = new Fixture();

            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<ManagerMapper>();
                config.AddProfile<AccessorMapper>();
            });
        }

        public void CreateTestDatabase()
        {
            //Set up a SQLite in-memory connection: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DotNetFlicksContext>()
                .UseSqlite(connection)
                .Options;

            Context = new DotNetFlicksContext(options) { };
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
