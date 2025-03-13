using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models;
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
            bool usernameExists = _userService.DoesUsernameExist(model.Username);
            bool emailExists = _userService.DoesEmailExist(model.Email);

            if (usernameExists)
            {
                ModelState.AddModelError("Username", "De gebruikersnaam is al in gebruik.");
            }

            if (emailExists)
            {
                ModelState.AddModelError("Email", "Het emailadres is al in gebruik.");
            }

            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            if (ModelState.IsValid)
            {
                var user = await _userService.RegisterUserAsync(model);
                var principal = _userService.CreateClaimsPrincipal(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("index", "home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            
            bool loginSuccessful = await _userService.LoginUserAsync(model.Username, model.Password, this);

            if (loginSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Onjuiste gegevens");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "account");
        }
    }
}
