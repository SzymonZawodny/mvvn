using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEventsApp.Helpers;
using CoursesApp.Models;

namespace CoursesApp.ViewModels
{
    public class StepViewModel : CourseBaseViewModel
    {
        public CourseModel courseModel;
        public StepModel stepModel;

        public StepViewModel(INavigationHelper navigationHelper, CourseListModel coursesList, CourseModel course, StepModel step) : base(navigationHelper)
        {
            coursesModel = coursesList;
            courseModel = course ?? new CourseModel();
            selectedCourse = course;
            stepModel = step ?? new StepModel();
            selectedStep = step;
        }
    }
}
//pamieta z jakiego kursu, z jakiej listy, i dostaje info w jaki krok wszedlem
//v.model dostaje informacje o poprzednim stanie i na jakim konkretnym przypadku/ modelu ma pracowac(w stepviwemodel na jakim kroku)