using AutoMapper;
using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Managers.Config;
using CoreTemplate.Web.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.Tests.IntegrationTests
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