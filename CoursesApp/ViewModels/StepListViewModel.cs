using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEventsApp.Helpers;
using CoursesApp.Models;
using System.Diagnostics;

namespace CoursesApp.ViewModels
{
    public class StepListViewModel : CourseBaseViewModel
    {
        public CourseModel courseModel { get; set; }
        public StepListViewModel(INavigationHelper navigationHelper, CourseListModel coursesList, CourseModel course) : base(navigationHelper)
        {
            coursesModel = coursesList;
            courseModel = course ?? new CourseModel();
            selectedCourse = course;
        }
    }
}
//panieta liste kursow do ktorej zostal przekazany, kurs dla ktorego ma wyswietlic liste krokow, 
//dpstaje info do jakiej listy krokow przechodzi