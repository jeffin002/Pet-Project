using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public bool HasMobile { get; set; }
        public static string ParentName { get; set; }

        public bool CanVote { get; set; }




    }
}
