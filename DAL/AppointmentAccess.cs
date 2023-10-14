using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO.Compression;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public class AppointmentAccess
    {
        private readonly string _connectionString;
        private ILogger<AppointmentAccess> _logger;

        public AppointmentAccess(IConfiguration config,ILogger<AppointmentAccess> logger)
        {
            _connectionString = config.GetConnectionString("MyConnection");
            _logger = logger;
        }

        public void AddAppointment(Appointment appointment)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand objSqlCommand = new SqlCommand("dbo.CreateAppointment", con);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.AddWithValue("@PetName", appointment.PetName);
                    objSqlCommand.Parameters.AddWithValue("@Description", appointment.Description);
                    objSqlCommand.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                    objSqlCommand.Parameters.AddWithValue("@BreedId", appointment.BreedId);
                    objSqlCommand.Parameters.AddWithValue("@StatusId", appointment.StatusId);
                    con.Open();
                    objSqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// Get Breed By PetTypeId From The DataBase
        /// </summary>
        /// <param name="petTypeId">id of a pet type</param>
        /// <returns></returns>
        public async Task<List<Breed>> GetBreedsByPetTypeId(int petTypeId)
        {
            List<Breed> breedList = new List<Breed>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("dbo.GetBreedTypesByPetTypeId", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", petTypeId);
                        SqlDataReader rdr = await command.ExecuteReaderAsync();
                        while (rdr.Read())
                        {
                            Breed brd = new Breed();
                            brd.Name = (string)rdr["Name"];
                            brd.Id = Convert.ToInt32(rdr["Id"]);
                            breedList.Add(brd);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                //return StatusCode(500, "An error occurred while processing the request.");
            }
            return breedList;
        }

       public async Task<List<Doctor>> AllDoctorList()
        {
            List<Doctor> doctorList = new List<Doctor>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("dbo.GetAllDoctors", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader rdr = await command.ExecuteReaderAsync();
                        while (rdr.Read())
                        {
                             Doctor dr = new Doctor();                            
                            dr.FirstName= (string)rdr["FirstName"];
                            dr.LastName= (string)rdr["LastName"];
                            dr.FullName= (string)rdr["FullName"];
                            dr.Id = Convert.ToInt32(rdr["Id"]);
                            doctorList.Add(dr);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return doctorList;
        }
        /// <summary>
        /// Gets the List of All Appointments From The Database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Appointment>> GetAllAppointments()
        {
            List<Appointment> appointlist = new List<Appointment>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("dbo.GetAllAppointments", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader rdr = await command.ExecuteReaderAsync();
                        while (rdr.Read())
                        {
                            Appointment apt = new Appointment();
                            apt.DoctorFullName = (string)rdr["DoctorFullName"];
                            apt.PetName = (string)rdr["PetName"];
                            apt.StatusName= (string)rdr["StatusName"];
                            apt.Id = Convert.ToInt32(rdr["Id"]);
                            apt.UpdatedDateTime = (DateTime)rdr["UpdatedDateTime"];
                            appointlist.Add(apt);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return appointlist;
        }
        public void DeleteAppointment(int deleteId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand objSqlCommand = new SqlCommand("dbo.DeleteAppointment", con);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.AddWithValue("@DeleteId", deleteId);                    
                    con.Open();
                    objSqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Appointment> GetAppointmentById(int id)
        {
            Appointment apt = new Appointment();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand objSqlCommand = new SqlCommand("dbo.GetAppointmentById", con);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader rdr = await objSqlCommand.ExecuteReaderAsync();
                    while (rdr.Read())
                    {                       
                        apt.Description = (string)rdr["Description"];
                        apt.PetName = (string)rdr["PetName"];
                        apt.DoctorFullName = (string)rdr["FullName"];
                        apt.Id = Convert.ToInt32(rdr["Id"]);
                        apt.DoctorId= Convert.ToInt32(rdr["DoctorId"]);                        
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return apt;
        }

        public async Task UpdateAppointmentById(Appointment appointment)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var parameters = new { Id = appointment.Id,
                                           DoctorId = appointment.DoctorId,
                                           Description = appointment.Description };

                    await connection.ExecuteAsync
                        (
                         "UpdateAppointmentById", parameters, commandType: System.Data.CommandType.StoredProcedure
                        );
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Appointment>> GetAllAppointmentsDapper(int currentPage,int pageSize)
        {
            IEnumerable<Appointment> appointlist = new List<Appointment>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    appointlist = await connection.QueryAsync<Appointment>
                        (
                         "GetAllAppointments",
                         new
                         {
                             currentPage,
                             pageSize
                         },
                         commandType: System.Data.CommandType.StoredProcedure
                        );
                    
                }               

            }
            catch (SqlException Sqlex)
            {
                _logger.LogError(Sqlex, "Error Occured While Calling the GetAllAppointments Method");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error Occured While Calling the GetAllAppointments Method");
            }            
            return appointlist.ToList();
        }


    }

}
