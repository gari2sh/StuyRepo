using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Models
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Subject#")]
        public int SubjectID { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }
        [Range(0, 4)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<SubjectAssignment> SubjectAssignment { get; set; }
    }
}
