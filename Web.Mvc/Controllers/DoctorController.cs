using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using System.Diagnostics;
using Web.Mvc.Models;

namespace Web.Mvc.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ILogger<DoctorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Doctor doctor = new Doctor();
            return View(doctor);
        }

        public IActionResult Privacy()
        {
            ViewData["Jeffin"] = "This is my test Privacy policy";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        
    }
}