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

namespace DAL
{
    public class AppointmentAccess
    {
        private readonly string _connectionString;
        public AppointmentAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MyConnection");
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
                    objSqlCommand.Parameters.AddWithValue("@Descripton", appointment.Description);
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

        public async Task<List<Breed>> GetBreedsByPetTypeId(int petTypeId)
        {
            List<Breed> breedList = new List<Breed>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (var command = new SqlCommand("dbo.GetBreedTypesByPetTypeId", con))
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
                    using (var command = new SqlCommand("dbo.AllDoctorList", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader rdr = await command.ExecuteReaderAsync();
                        while (rdr.Read())
                        {
                            Doctor dr = new Doctor();                            
                            dr.FirstName= (string)rdr["FirstName"];
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

    }
}
