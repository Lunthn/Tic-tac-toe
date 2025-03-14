using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Services
{
    public static class DbSeeder
    {
        public static void SeedAdmin(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                context.Database.Migrate();

                string? adminUsername = config["AdminSettings:AdminUsername"];
                string? adminPassword = config["AdminSettings:AdminPassword"];
                string? adminEmail = config["AdminSettings:AdminEmail"];

                if (string.IsNullOrEmpty(adminPassword) || string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminUsername))
                {
                    throw new Exception("Admin data not found");
                }

                var passwordHasher = new PasswordHasher<object>();
                string hashedPassword = passwordHasher.HashPassword(null, adminPassword);

                if (!context.Users.Any(u => u.Username == adminUsername))
                {
                    context.Users.Add(new User
                    {
                        Username = adminUsername,
                        PasswordHash = hashedPassword,
                        Role = "admin",
                        NumberOfWins = 0,
                        Email = adminEmail
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
