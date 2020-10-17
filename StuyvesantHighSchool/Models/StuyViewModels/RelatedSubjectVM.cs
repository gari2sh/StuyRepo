using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StuyvesantHighSchool.Models.StuyViewModels
{
    public class RelatedSubjectVM
    {

        public int SubjectID { get; set; }
        public string Title { get; set; }
        public bool RelatedSubject { get; set; }
    }
}
