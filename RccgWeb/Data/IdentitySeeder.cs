using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Models;

namespace RccgWeb.Data
{
    public static class IdentitySeeder
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await EnsureAdminAsync(userManager);
        }

        public static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.Users.Where(x => x.UserName == "admin@province4.local").SingleOrDefaultAsync();

            if (admin != null) return;

            admin = new ApplicationUser
            {
                UserName = "admin@province4.local",
                Email = "admin@province4.local",
                FirstName = "admin",
                LastName = "admin",
            };

            await userManager.CreateAsync(admin, "ProvincialAdmin");

            await userManager.AddToRoleAsync(admin, "Admin");
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync("Admin");
            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }
}