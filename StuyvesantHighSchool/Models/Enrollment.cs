using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Models
{

    public class Enrollment
    {
        [Display(Name = "Enrollment#")]
        public int EnrollmentID { get; set; }
        [Display(Name = "Subject#")]
        public int SubjectID { get; set; }
        [Display(Name = "Student#")]
        public int StudentID { get; set; }

        public int Score { get; set; }

        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}
