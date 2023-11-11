using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System.Threading.Tasks;
using Utility;
using Web.Mvc.Models;

namespace Web.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _factory;

        public LoginController(IConfiguration config, ILoggerFactory factory)
        {
            _config = config;
            _factory = factory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var vm = new LoginViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel request)
        {
            var userAccesss = new AppUserAccess(_config, _factory.CreateLogger<AppUserAccess>());
            var appUser = await userAccesss.GetAppUserByUserName(request.UserName);
            if(appUser != null)
            {
                var isValidUser = CryptoHelper.VerifyPassword(request.Password, appUser.PasswordHash, appUser.Salt);
                if (isValidUser)
                    return RedirectToAction("GetAllAppointments", "Appointment", new { currentPage = 1, pageSize = 10 });
            } 
            return Json(new { message = "Invalid username or Password." });
        }

        [HttpGet]
        public IActionResult Register()
        {
            var vm = new AppUser();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] AppUser user)
        {
            var userAccesss = new AppUserAccess(_config, _factory.CreateLogger<AppUserAccess>());
            var passwordHashAndSalt = CryptoHelper.GeneratePasswordHashAndSalt(user.PasswordText);
            user.PasswordHash = passwordHashAndSalt.Item1;
            user.Salt = passwordHashAndSalt.Item2;
            await userAccesss.CreateAppUser(user);
            return RedirectToAction("Login");
        }

    }
}
