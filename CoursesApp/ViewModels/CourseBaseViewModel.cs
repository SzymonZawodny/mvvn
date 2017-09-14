using CoursesApp.Models;
using CoursesApp.Views;
using GameEventsApp.Helpers;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace CoursesApp.ViewModels
{
    public class CourseBaseViewModel
    {
        private INavigationHelper navigationHelper;

        private ICommand _navigateToCourseListViewCommand;
        private ICommand _navigateToStepListViewCommand;
        private ICommand _navigateToStepViewCommand;
        private ICommand _updateCoursesCommand;
        private ICommand _getCoursesCommand;

        public CourseModel selectedCourse { get; set; }
        public StepModel selectedStep { get; set; }

        

        public ICommand navigateToCourseListViewCommand
        {
            get
            {
                return _navigateToCourseListViewCommand ?? (_navigateToCourseListViewCommand =
                    new Command((object args) =>
                    {
                        navigationHelper.NavigateTo(typeof(CourseListView), this);
                    }));
            }
        }

        public ICommand getCoursesCommand
        {
            get
            {
                return _getCoursesCommand ?? (_getCoursesCommand =
                    new Command((object args) =>
                    {
                        DbInit();
                    }));
            }
        }
        //nav to concret course
        public ICommand navigateToStepListViewCommand
        {
            get
            {
                return _navigateToStepListViewCommand ?? (_navigateToStepListViewCommand =
                    new Command((object args) =>
                    {
                        stepList(selectedCourse);
                       
                    }));
            }
        }

        public ICommand navigateToStepViewCommand
        {
            get
            {
                return _navigateToStepViewCommand ?? (_navigateToStepViewCommand =
                    new Command((object args) =>
                    {
                        stepDetails(selectedCourse, selectedStep);
                    }));
            }
        }

        public ICommand updateCoursesCommand
        {
            get
            {
                return _updateCoursesCommand ?? (_updateCoursesCommand =
                    new Command(async(object args) =>
                    {
                        await updateDatabase();
                        navigationHelper.NavigateTo(typeof(CourseListView), this);
                    }));
            }
        }



        private string dbFileName = "db.Courses";
        private SQLiteConnection connection;
        private CourseListModel coursesFromApi;

        protected CourseListModel _courseListModel;

        protected CourseListModel coursesModel
        {
            get
            {
                return _courseListModel;
            }
            set
            {
                _courseListModel = value;
            }
        }

        public CourseBaseViewModel(INavigationHelper navigationHelper)
        {
            if (navigationHelper == null) throw new ArgumentNullException();
            this.navigationHelper = navigationHelper;
        }

        private async void DbInit()
        {

            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbFileName);
            connection = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath);
            connection.CreateTable<CourseModel>();
            connection.CreateTable<StepModel>();
            Debug.WriteLine(connection.Table<CourseModel>().ToList().Count());
            //connection.DeleteAll<CourseModel>();
            //connection.DeleteAll<StepModel>();
            

            if (connection.Table<CourseModel>().ToList().Count() == 0 && connection.Table<StepModel>().ToList().Count == 0)
            {
                await getCourses();
                foreach (var c in coursesFromApi)
                {
                    connection.Insert(c);
                    foreach(var s in c.steps)
                    {
                        connection.Insert(s);
                       // Debug.WriteLine(s.description);
                    }
                    _courseListModel.Add(c);
                    Debug.WriteLine(c.name);
                    Debug.WriteLine(c.steps.Count());
                }
            }
            else
            {
                var coursesList = connection.Table<CourseModel>().ToList();
                var stepList = connection.Table<StepModel>().ToList();
                foreach (var c in coursesList)
                {
                    CourseModel kurs = new CourseModel();
                    kurs.courseId = c.courseId;
                    kurs.name = c.name;
                    kurs.completed = c.completed;
                    kurs.steps = new ObservableCollection<StepModel>();
                    foreach (var s in stepList)
                    {
                        if (s.courseId == c.courseId)
                        {
                            StepModel krok = new StepModel();
                            krok.stepId = s.stepId;
                            krok.stepNumber = s.stepNumber;
                            krok.description = s.description;
                            krok.courseId = s.courseId;
                            krok.completed = s.completed;
                            kurs.steps.Add(krok);
                        }
                    }
                    _courseListModel.Add(kurs);
                    Debug.WriteLine(c.name);
                }
            }
        }
        // api connections
        public async Task getCourses()
        {
            HttpClient httpClient = new HttpClient();
            string http = string.Format(("http://localhost:51799//api/courses1/"));
            Uri uri = new Uri(http);
            var response = await httpClient.GetAsync(uri);
            string contentString = await response.Content.ReadAsStringAsync();
            coursesFromApi =  JsonConvert.DeserializeObject<CourseListModel>(contentString);
        }
        //nav to stepList and create v.m
        private void stepList(CourseModel course)
        {
            StepListViewModel stepListViewModel = new StepListViewModel(navigationHelper, coursesModel, course);
            navigationHelper.NavigateTo(typeof(StepListView), stepListViewModel);
        }
        //moving to concret step from stepList, v.m for concret step
        private void stepDetails (CourseModel course, StepModel step)
        {
            StepViewModel stepViewModel = new StepViewModel(navigationHelper, coursesModel, course, step);
            navigationHelper.NavigateTo(typeof(StepView), stepViewModel);
        }
        // db
        private async Task updateDatabase()
        {
            await getCourses();
            CourseModel checking;
            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbFileName);
            connection = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath);
            var data = connection.Table<CourseModel>().ToList();
            int count = data.Count;
            bool stop = false;
            if (coursesFromApi.Count > count)
            {
                foreach (var c in coursesFromApi)   
                {
                    checking = new CourseModel();
                    checking = data.Where(d => d.courseId == c.courseId).FirstOrDefault();
                    if (checking != null)
                    {
                            
                    }
                    else
                    {
                        connection.Insert(c);
                        foreach (var s in c.steps)
                        {
                         connection.Insert(s);
                                // Debug.WriteLine(s.description);
                        }
                        _courseListModel.Add(c);
                        stop = true;
                    }
                    if (stop)
                    {
                        break;
                    }   
                }
            }
        }
    }
}

