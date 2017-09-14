using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesApi
{
    public class DTOs
    {
    }

    public class StepDTO
    {
        public int stepId { get; set; }
        public int courseId { get; set; }
        public int stepNumber { get; set; }
        public string description { get; set; }
        public bool completed { get; set; }
    }

    public class CourseDTO
    {
        public int courseId { get; set; }
        public string name { get; set; }
        public List<StepDTO> steps { get; set; }
        public bool completed { get; set; }
    }
}