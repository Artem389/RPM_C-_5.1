using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MedelStoreContext _context;

        public IActionResult AccessDenied()
        {
            return View();
        }

        public HomeController(ILogger<HomeController> logger, MedelStoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Проверяем, есть ли пользователь с таким логином и паролем
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Name == model.Login);
                var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Name == model.Login);

                if (client != null || staff != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    if (client != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Name, client.Name));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Client"));
                    }
                    else if (staff != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Name, staff.Name));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Staff"));
                    }

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Неверный логин или пароль");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role == "Client")
                {
                    var client = new Client
                    {
                        Name = model.Login,
                        Suname = "NewUser",
                        Fatherland = "NewUser"
                    };

                    _context.Clients.Add(client);
                }
                else if (model.Role == "Staff")
                {
                    var staff = new Staff
                    {
                        Name = model.Login,
                        Suname = "NewUser",
                        Fatherland = "NewUser",
                        PositionsId = 1 // Базовая позиция
                    };

                    _context.Staff.Add(staff);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("SignIn", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}