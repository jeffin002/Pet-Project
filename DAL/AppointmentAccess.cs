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
            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                try
                {
                    SqlCommand objSqlCommand = new SqlCommand("dbo.CreateAppointment", con);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.AddWithValue("@Descripton", appointment.Description);
                    objSqlCommand.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                    objSqlCommand.Parameters.AddWithValue("@PetId", appointment.PetId);
                    objSqlCommand.Parameters.AddWithValue("@StatusId", appointment.StatusId);
                    con.Open();
                    objSqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
               
            }
            //using (SqlConnection con = new SqlConnection(_connectionString))
            //{


            //    try
            //    {
            //        SqlCommand objSqlCommand = new SqlCommand("dbo.GetBreedTypeListByPetTypeId", con);
            //        objSqlCommand.CommandType = CommandType.StoredProcedure;
            //        objSqlCommand.Parameters.AddWithValue("@Id", ) ;                   
            //        con.Open();
            //        objSqlCommand.ExecuteNonQuery();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }

            //}

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
                        command.Parameters.AddWithValue("@Id",petTypeId);
                        SqlDataReader rdr=await command.ExecuteReaderAsync();
                        while (rdr.Read()) 
                        {
                            Breed brd =new Breed();
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
    }

}
