using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Models
{
    public class SubjectAssignment
    {
        public int TeacherID { get; set; }
        public int SubjectID { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}
