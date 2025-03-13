using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Services; 
namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=game.db"));

           
            builder.Services.AddScoped<UserService>();

           
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

           
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

          
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

           
            app.UseHttpsRedirection();
            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

           
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
