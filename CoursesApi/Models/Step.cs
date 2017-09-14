using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesApi.Models
{
    public class Step
    {
        public int StepID { get; set; }
        public int CourseID { get; set; }
        public int numberOfStep { get; set; }
        public string description { get; set; }

        public virtual Course course { get; set; }
    }
}