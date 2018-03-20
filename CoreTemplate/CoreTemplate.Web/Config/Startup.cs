using AutoMapper;
using CoreTemplate.Accessors.Config;
using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Identity;
using CoreTemplate.Common.Configuration;
using CoreTemplate.Engines.Config;
using CoreTemplate.Managers.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.Web.Config
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            //Set up Configuration (for access to appsettings.json)
            //Note: CoreTemplate uses a local SMTP server, but in production, you'd want to use something like SendGrid, which offers 100 free emails a day: https://sendgrid.com/free/
            services.AddOptions();
            services.Configure<EmailConfiguration>(Configuration.GetSection("Email"));

            //Set up database
            services.AddDbContext<CoreTemplateContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CoreTemplateConnection")));

            //Set up Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CoreTemplateContext>()
                .AddDefaultTokenProviders();

            //Set up "ELM" (Error Logging Middleware)
            services.AddElm();

            //Set up MVC
            services.AddMvc();

            //Set up dependency injection
            services.AddManagerDependencies();
            services.AddEngineDependencies();
            services.AddAccessorDependencies();

            //Set up all AutoMapper mappings
            Mapper.Initialize(config => {
                config.AddProfile<ManagerMapper>();
                config.AddProfile<AccessorMapper>();
            });
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Capture errors and add an endpoint to see the error log via ELM (Error Logging Middleware)
            app.UseElmPage();
            app.UseElmCapture();
        }
    }
}
