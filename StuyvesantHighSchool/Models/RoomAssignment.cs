using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Models
{
    public class RoomAssignment
    {
        [Key]
        public int TeacherID { get; set; }
        [StringLength(50)]
        [Display(Name = "Teacher's Room#")]
        public string Room { get; set; }

        public Teacher Teacher { get; set; }
    }
}
