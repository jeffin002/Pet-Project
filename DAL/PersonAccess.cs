
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class PersonAccess
    {
        public string ConnectionString { get; set; }
        public void AddPerson(Person person)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                

                try
                {
                    SqlCommand objSqlCommand = new SqlCommand("dbo.CreatePerson", con);
                    objSqlCommand.CommandType = CommandType.StoredProcedure;
                    objSqlCommand.Parameters.AddWithValue("@FirstName", person.FirstName);
                    objSqlCommand.Parameters.AddWithValue("@LastName", person.LastName);
                    objSqlCommand.Parameters.AddWithValue("@Email", person.Email);
                    objSqlCommand.Parameters.AddWithValue("@Age", person.Age);
                    objSqlCommand.Parameters.AddWithValue("@FamilyName", person.FamilyName);
                    objSqlCommand.Parameters.AddWithValue("@CanVote", person.CanVote);


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
