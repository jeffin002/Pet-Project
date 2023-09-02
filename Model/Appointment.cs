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
        public int PetTypeId { get; set; }
        public int StatusId { get; set; }
        public string PetName { get; set; }
        public string DoctorName { get; set; }
        public string PetBreed { get; set; }
        public string PetType { get; set; }
        public int BreedId { get; set; }


    }
}
