using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Diagnostics;



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
            //ViewData["Date"] = DateOnly.FromDateTime(DateTime.Now);
            
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Jeffin"] = "This is my test Privacy policy";
            return View();
        }
      
       
    }
}