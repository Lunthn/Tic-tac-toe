using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TicTacToe.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool DoesUsernameExist(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public bool DoesEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public async Task<User> RegisterUserAsync(RegisterModel model)
        {
            var hashedPassword = UserService.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = hashedPassword,
                Role = "player",
                NumberOfWins = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public ClaimsPrincipal CreateClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

   
        public async Task<bool> LoginUserAsync(string username, string password, Controller controller)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(user.PasswordHash, password))
            {
                var principal = CreateClaimsPrincipal(user);

                await controller.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return true;
            }

            return false;
        }

        public static string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            try
            {
                var passwordHasher = new PasswordHasher<object>();
                var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
                return result == PasswordVerificationResult.Success;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
    }
}
