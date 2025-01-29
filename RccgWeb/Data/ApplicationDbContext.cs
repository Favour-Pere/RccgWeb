using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Models;

namespace RccgWeb.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Church> Churches { get; set; }

        public DbSet<Pastor> Pastors { get; set; }

        public DbSet<Offering> Offerings { get; set; }

        public DbSet<Tithe> Tithes { get; set; }

        public DbSet<Workers> Workers { get; set; }

        public DbSet<SpecialProgram> SpecialPrograms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}