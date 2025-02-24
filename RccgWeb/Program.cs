using RccgWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Services.Interfaces;
using RccgWeb.Services;
using RccgWeb.Models;

namespace RccgWeb
{
    public class Program
    {
        public static void InitializeDatabase(IHost host)
        {
            using var scopee = host.Services.CreateScope();

            var services = scopee.ServiceProvider;

            try
            {
                IdentitySeeder.InitializeAsync(services).Wait();
            }
            catch (Exception error)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogError(error, "An error occured while seeding the database.");
            }
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RccgConnectionStrings"));
            }
           );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IAreaService, AreaService>();
            builder.Services.AddScoped<IChurchAdminService, ChurchAdminService>();
            //builder.Services.AddDefaultIdentity<ApplicationUser>(
            //    options =>
            //    {
            //        options.SignIn.RequireConfirmedPhoneNumber = false;
            //        options.SignIn.RequireConfirmedAccount = false;

            //        options.User.RequireUniqueEmail = true;
            //    }
            //);
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //});

            var app = builder.Build();
            InitializeDatabase(app);
            app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}