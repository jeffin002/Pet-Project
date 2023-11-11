using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Register
    {       
        public int id {  get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }
        public string Email { get; set; }       
        public string Password { get; set; }       
        public string PasswordSalt { get; set; }
        public string ConfirmPassword { get; set; }       
        public string Username { get; set; }

    }
}
