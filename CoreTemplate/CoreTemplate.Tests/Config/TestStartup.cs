using AutoMapper;
using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Managers.Config;
using CoreTemplate.Web.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.Tests.Config
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

            Mapper.Initialize(cfg => {
                cfg.AddProfile<AccessorMapper>();
                cfg.AddProfile<ManagerMapper>();
            });
        }
    }
}