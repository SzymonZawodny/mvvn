using CoursesApp.Models;
using GameEventsApp.Helpers;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApp.ViewModels
{
    public class CoursesListViewModel : CourseBaseViewModel
    {
        public CoursesListViewModel(INavigationHelper navigationHelper) : base(navigationHelper)
        {
            _courseListModel = new CourseListModel();
        }

        public ObservableCollection<CourseModel> courses
        {
            get
            {
                return _courseListModel;
            }
        }
    }
}
//v.model dla listy kursow