using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolService
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public List<Class> Classes { get; set; }
    }
}
