using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DoctorAccess
    {
        public string ConnectionString { get; set; }
        public void AddDoctor(Doctor doctor)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
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
