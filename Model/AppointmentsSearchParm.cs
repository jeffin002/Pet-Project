using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AppointmentsSearchParm
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
