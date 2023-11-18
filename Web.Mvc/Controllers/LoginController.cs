using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Utility;
using Web.Mvc.Helper;
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
            var outputParams=await userAccesss.CreateAppUser_V2(user);
            Guid emailGuid = outputParams.Item1;
            string content = outputParams.Item2;
            string callbackUrl = $"https://localhost:7103/Login/Verify?id={emailGuid}";
            List<string> mailaddress=new List<string>();
            mailaddress.Add(user.Email);
            string emailContent = content.Replace("{ApplicationUrl}", callbackUrl);
           // string emailContent=string.Format("Congrats!. You have successfully registed on PetProject. Please click the link {0} to verify your email.", );
            Message msg = new Message(mailaddress,"Registration Success",emailContent,_config);
            EmailHelper email = new EmailHelper(_config);
            await email.SendEmailAsync(msg);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Verify([FromQuery]Guid id)
        {
            var vm = new AppUser();

            return View(vm);
        }


    }
}
