using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesApi.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string name { get; set; }

        public virtual ICollection<Step> steps { get; set; }
    }
}