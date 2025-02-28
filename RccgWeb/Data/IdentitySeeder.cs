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
                Id = Guid.NewGuid().ToString(),
                UserName = "admin@province4.local",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@province4.local",
                PhoneNumber = "07040672132",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Provincial@Admin4");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }

                return;
            }

            admin = await userManager.FindByEmailAsync("admin@province4.local");

            if (admin != null)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync("Admin");
            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }
}