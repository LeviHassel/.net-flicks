using CoreTemplate.Managers.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(Configuration);

            services.AddMvc();

            services.ConfigureDependencies();

            CoreTemplateConfiguration.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            /*
             * TODO: Figure out why this doesn't work
             * //Seed data
             * CoreTemplateContextSeed.SeedAsync(app).Wait();
             */

            /*
             * TODO: Implement this
             * //Seed user
             * var defaultUser = new ApplicationUser { UserName = "admin@coretemplate.com", Email = "admin@coretemplate.com" };
             * userManager.CreateAsync(defaultUser, "Pass@word1").Wait();
             */
        }
    }
}
