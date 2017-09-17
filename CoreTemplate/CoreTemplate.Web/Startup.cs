using AutoMapper;
using CoreTemplate.Accessors;
using CoreTemplate.Accessors.Models;
using CoreTemplate.Web.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTemplate.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CoreTemplateContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /*
             * TODO: Add testing connection setup here (see the line above)
             */

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CoreTemplateContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAutoMapper();

            // Add application services
            services.AddManagerDependencies();
            services.AddAccessorDependencies();
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
