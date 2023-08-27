using DAL;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
//using System.Web.Mvc;
using Web.Mvc.Models;

namespace Web.Mvc.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IConfiguration _config;
        private string _connectionString;
        private SqlConnection connection;

        public AppointmentController(ILogger<AppointmentController> logger, IConfiguration config)
        {
            _logger = logger;
            _config=config;
        }

        [HttpGet]        
        public IActionResult Index()
        {
            Appointment appointment = new Appointment();
            return View("appointment", appointment);
        }

        [HttpPost]
        public IActionResult Index([FromBody] Appointment appointment)
        {
            try
            {
                AppointmentAccess appointmentAccess = new AppointmentAccess(_config);
                appointmentAccess.AddAppointment(new Appointment());
                return Json(new { message = "success"});
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { messege = "Error", discription = "an error occured while processing ur request" });

            }
        }
       
        [HttpGet]
        public IActionResult PetId()
        {           
            var viewModel = new PetTypeModel();
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> PetId([FromBody] PetTypeModel model)
        {
            if (model == null || model.PetTypeValue < 1 || model.PetTypeValue > 3)
            {
                return BadRequest("Invalid pet type value.");
            }

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("dbo.GetBreedTypeListByPetTypeId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PetTypeValue", model.PetTypeValue);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
       

        [HttpGet]
        public IActionResult GetPetTypes()
        {
            // Replace this sample data with data from your database
            var petTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Dog" },
            new SelectListItem { Value = "2", Text = "Cat" },
            new SelectListItem { Value = "3", Text = "Rabbit" }
        };

            return Json(petTypes);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetBreedsByPetTypeId([FromQuery]int petTypeId)
        {
            AppointmentAccess a = new AppointmentAccess(_config);

            List<Breed> breedList = await a.GetBreedsByPetTypeId(petTypeId);
            return Json(breedList);
        }
    }
}

    