using DAL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {        

        private readonly ILogger<AppointmentController> _logger;
        private IConfiguration _config;
        private ILoggerFactory _factory;

        public AppointmentController(ILogger<AppointmentController> logger, IConfiguration config, ILoggerFactory factory)
        {
            _logger = logger;
            _config = config;
            _factory = factory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AppointmentAccess aa = new AppointmentAccess(_config, _factory.CreateLogger<AppointmentAccess>());            
            var doctorList = await aa.AllDoctorList();
            return Ok(doctorList);
        }
    }
}