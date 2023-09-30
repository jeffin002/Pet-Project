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

    }
}
