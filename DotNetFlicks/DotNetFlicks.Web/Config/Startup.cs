using AutoMapper;
using DotNetFlicks.Accessors.Config;
using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.Common.Models;
using DotNetFlicks.Engines.Config;
using DotNetFlicks.Managers.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetFlicks.Web.Config
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
            //Note: DotNetFlicks uses a local SMTP server, but in production, you'd want to use something like SendGrid, which offers 100 free emails a day: https://sendgrid.com/free/
            services.AddOptions();
            services.Configure<EmailConfiguration>(Configuration.GetSection("Email"));

            //Set up database
            services.AddDbContext<DotNetFlicksContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DotNetFlicksConnection")));

            //Set up Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DotNetFlicksContext>()
                .AddDefaultTokenProviders();

            //Set up "ELM" (Error Logging Middleware)
            services.AddElm();

            //Allow submission of forms with up to 10,000 data points (default is 1024)
            services.Configure<FormOptions>(x => x.ValueCountLimit = 10000);

            //Set up MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
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
