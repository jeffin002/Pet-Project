using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public int SelectedDoctorId { get; set; }
        public int PetId { get; set; }
        public string Description { get; set; }
        public int DoctorId { get; set; }
        public int PetTypeId { get; set; }
        public int StatusId { get; set; }

        [DisplayName("Pet Name")]
        public string PetName { get; set; }
        public string DoctorName { get; set; }
        public string PetBreed { get; set; }
        public string PetType { get; set; }
        public int BreedId { get; set; }

        [DisplayName("Doctor Name")]
        public string DoctorFullName { get; set; }

        [DisplayName("Status")]
        public string StatusName { get; set; }
        public DateTime CreatedDateTime { get; set; }

        [DisplayName("Updated On")]
        public DateTime UpdatedDateTime { get; set;}
        public IEnumerable<SelectListItem> DoctorList { get; set; }
        public List<Doctor> EditList { get; set; }

    }
}
