using Model;
using System.Collections.Generic;
using System.Linq;
using Custom = Microsoft.AspNetCore.Mvc.Rendering;//this is called NameSpace aliasing 

namespace Web.Mvc.Extentions
{
    public static class Extentions
    {
        public static IEnumerable<Custom.SelectListItem> ToViewModel(this IEnumerable<Doctor> items) 
        {
            if (items == null)
                return null;
            IEnumerable<Custom.SelectListItem> result = items.Select(d => new Custom.SelectListItem
            {
                Text = d.FullName,
                Value = d.Id.ToString()                
            }) ;
            return result ;
        }
        public static List<Custom.SelectListItem> ToViewModel(this IEnumerable<Status> items)
        {
            if (items == null)
                return null;
            List<Custom.SelectListItem> result = items.Select(s => new Custom.SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();
            result.Insert(0, new Custom.SelectListItem
            {
                Text = "All",
                Value = 0.ToString()
            });
            return result;
        }
        public static List<Custom.SelectListItem> ToDoctorViewModel(this IEnumerable<Doctor> items)
        {
            if (items == null)
                return null;
            List<Custom.SelectListItem> result = items.Select(s => new Custom.SelectListItem
            {
                Text = s.FullName,
                Value = s.Id.ToString()
            }).ToList();
            result.Insert(0, new Custom.SelectListItem
            {
                Text = "All",
                Value = 0.ToString()
            });
            return result;
        }

    }
}
