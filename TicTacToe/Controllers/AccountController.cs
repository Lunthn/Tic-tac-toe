using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (_userService.DoesUsernameExist(model.Username))
            {
                ModelState.AddModelError("Username", "De gebruikersnaam is al in gebruik.");
                return View(model);
            }

            if (_userService.DoesEmailExist(model.Email))
            {
                ModelState.AddModelError("Email", "Het emailadres is al in gebruik.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var user = await _userService.RegisterUserAsync(model);

                var principal = _userService.CreateClaimsPrincipal(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            bool loginSuccessful = await _userService.LoginUserAsync(username, password, this);

            if (loginSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
