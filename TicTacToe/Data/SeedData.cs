using Microsoft.AspNetCore.Identity;
using TicTacToe.Data.Entities;

namespace TicTacToe.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("administrator"))
                {
                    await roleManager.CreateAsync(new IdentityRole("administrator"));
                }

                var adminEmail = "admin@admin.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    var newUser = new User
                    {
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true 
                    };

                    var result = await userManager.CreateAsync(newUser, "P@ssw0rd!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, "administrator");
                    }
                    else
                    {
                        throw new Exception($"Error seeding admin-user: {string.Join(", ", result.Errors)}");
                    }
                }
            }
        }
    }
}

