using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RccgWeb.Models;

namespace RccgWeb.Data
{
    public static class IdentitySeeder
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await EnsureAdminAsync(userManager);
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await EnsureRolesAsync(roleManager);
        }

        public static async Task EnsureAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.Users.Where(x => x.UserName == "admin@province4.local").SingleOrDefaultAsync();

            if (admin != null) return;

            admin = new ApplicationUser
            {
                UserName = "admin@province4.local",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@province4.local",
                PhoneNumber = "07040672132",
                ChurchId = null
            };

            var result = await userManager.CreateAsync(admin, "ProvincialAdmin");

            var result2 = await userManager.AddToRoleAsync(admin, "Admin");

            Console.WriteLine(result);
            Console.WriteLine(result2);
        }

        public static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync("Admin");
            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }
}