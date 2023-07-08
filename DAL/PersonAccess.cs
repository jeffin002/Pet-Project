
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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand objSqlCommand = new SqlCommand("GetEmployeeList", con);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {


                }
                catch (Exception ex)
                {

                }
            }
        }
    }

}
