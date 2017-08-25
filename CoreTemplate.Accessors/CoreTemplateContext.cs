using CoreTemplate.Accessors.Identity;
using CoreTemplate.Accessors.Models.EF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Accessors
{
  public class CoreTemplateContext : IdentityDbContext<ApplicationUser>
    {
      public CoreTemplateContext(DbContextOptions<CoreTemplateContext> options)
          : base(options)
      {
      }

      protected override void OnModelCreating(ModelBuilder builder)
      {
          base.OnModelCreating(builder);
          // Customize the ASP.NET Identity model and override the defaults if needed.
          // For example, you can rename the ASP.NET Identity table names and more.
          // Add your customizations after calling base.OnModelCreating(builder);
      }

      public DbSet<Movie> Movies { get; set; }
  }
}
