using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;

namespace CoursesApp.Models
{
    public class CourseListModel : ObservableCollection<CourseModel>
    {
        public CourseListModel()
        {
        }
    }
}
