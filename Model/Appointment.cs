using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public int PetId { get; set; }
        public int StatusId { get; set; }

    }
}
