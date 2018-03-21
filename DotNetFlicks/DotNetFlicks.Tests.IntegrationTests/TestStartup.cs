using AutoMapper;
using DotNetFlicks.Accessors.Config;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Managers.Config;
using DotNetFlicks.Web.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetFlicks.Tests.IntegrationTests
{
    public class TestStartup : Startup
    {
        public DotNetFlicksContext Context { get; private set; }

        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureDatabase(Configuration);

            //Set up MVC
            services.AddMvc();

            // Set up dependency injection
            services.AddManagerDependencies();
            services.AddAccessorDependencies();

            Mapper.Initialize(config => {
                config.AddProfile<AccessorMapper>();
                config.AddProfile<ManagerMapper>();
            });
        }
    }
}