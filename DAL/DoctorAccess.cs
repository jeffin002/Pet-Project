using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class DoctorAccess
    {
        private readonly string _connectionString;
        public DoctorAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MyConnection");
        }
       
        public void AddDoctor(Doctor doctor)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                try
                {
                    SqlCommand objSqlCommand = new SqlCommand("dbo.CreateDoctor", con);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                    objSqlCommand.Parameters.AddWithValue("@LastName", doctor.LastName);
                    objSqlCommand.Parameters.AddWithValue("@Email", doctor.Email);



                    con.Open();
                    objSqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
