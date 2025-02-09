using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Models;

namespace RccgWeb.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Zone> Zones { get; set; }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Parish> Parishes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            GenerateChurchIds<Zone>();
            GenerateChurchIds<Area>();
            GenerateChurchIds<Parish>();
            return base.SaveChanges();
        }

        private void GenerateChurchIds<T>() where T : class
        {
            foreach (var entry in ChangeTracker.Entries<T>().Where(e => e.State == EntityState.Added))
            {
                var churchIdProperty = entry.Entity.GetType().GetProperty("ChurchId");
                if (churchIdProperty != null && churchIdProperty.GetValue(entry.Entity) == null)
                {
                    var generatedId = ChurchIdGenerator.GenerateChurchId<T>(this);
                    churchIdProperty.SetValue(entry.Entity, generatedId);
                }
            }
        }
    }
}