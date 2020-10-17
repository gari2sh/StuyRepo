using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        public int? TeacherID { get; set; }

        public Teacher Admin { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
