using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Web.Mvc.Models;

namespace Web.Mvc.Controllers
{
    public class HomeController : Controller 
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Date"] = DateOnly.FromDateTime(DateTime.Now);
            
            return View();
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