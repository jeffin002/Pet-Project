using Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Mvc.Extentions
{
    public static class Extentions
    {
        public static IEnumerable<SelectListItem> ToViewModel(this IEnumerable<Doctor> items) 
        {
            if (items == null)
                return null;
            IEnumerable<SelectListItem> result = items.Select(d => new SelectListItem
            {
                Text = d.FullName,
                Value = d.Id.ToString()                
            }) ;
            return result ;
        }

    }
}
