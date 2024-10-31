using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolService
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public School School { get; set; }
        public List<Student> Students { get; set; }
    }
}
