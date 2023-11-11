using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using System.Collections.Generic;

namespace Web.Mvc.Models
{
    public class AppointmentListContainer
    {
        public List<Appointment> AppointmentList { get; set; }

        public Pager PagingInfo { get; set; }

        public AppointmentSearchParms SearchParms { get; set; }
    }
    public class AppointmentSearchParms
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int SelectedStatusId { get; set; }
        public int SelectedDoctorId { get; set; }
        //public string SearchTerm { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> DoctorList { get; set; }


        //public Address SearchAddress { get; set; }

    }
    //public class Address
    //{
    //    public string Address1 { get; set; }
    //}
}
