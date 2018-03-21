using DotNetFlicks.Accessors.Identity;
using DotNetFlicks.Accessors.Models.EF;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DotNetFlicks.Accessors.Database
{
    public class DotNetFlicksContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Default context
        /// </summary>
        /// <param name="options"></param>
        public DotNetFlicksContext(DbContextOptions<DotNetFlicksContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //Disable cascade deletions
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public virtual void SetState(object entity, EntityState state)
        {
            this.Entry(entity).State = state;
        }

        /// <summary>
        /// Detach all entries in the Context's Change Tracker.
        /// Very useful for avoiding tracking errors in testing.
        /// </summary>
        public virtual void DetachAll()
        {
            foreach (var entry in this.ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
        }

        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
    }
}
