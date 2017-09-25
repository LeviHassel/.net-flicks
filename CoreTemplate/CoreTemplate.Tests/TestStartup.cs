using AutoMapper;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Managers.Config;
using CoreTemplate.Web.Config;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.Tests
{
    public class TestStartup : Startup
    {
        public CoreTemplateContext Context { get; private set; }

        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureDatabase(Configuration);

            services.AddMvc();

            services.ConfigureDependencies();

            services.AddAutoMapper();
        }
    }
}