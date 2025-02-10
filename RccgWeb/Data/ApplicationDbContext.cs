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

        public DbSet<ProgramActivity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProgramActivity>()
           .HasOne(a => a.Zone)
           .WithMany() // No navigation back from Zone to Activity
           .HasForeignKey(a => a.ZoneId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramActivity>()
                .HasOne(a => a.Area)
                .WithMany() // No navigation back from Area to Activity
                .HasForeignKey(a => a.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramActivity>()
                .HasOne(a => a.Parish)
                .WithMany(p => p.ProgramActivities)  // Navigation property in Parish
                .HasForeignKey(a => a.ParishId)
                .OnDelete(DeleteBehavior.Restrict);
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
                    var generatedId = ChurchIdGenerator.GenerateChurchId(this);

                    churchIdProperty.SetValue(entry.Entity, generatedId);
                }
            }
        }
    }
}