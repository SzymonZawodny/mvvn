using CoursesApp.Models;
using CoursesApp.ViewModels;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CoursesApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StepView : Page
    {
        StepViewModel stepViewModel;
        int i, count;
        StepModel step = new StepModel();
        private string dbFileName = "db.Courses";
        private SQLiteConnection connection;

        public StepView()
        {
            this.InitializeComponent();
            //cos();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            stepViewModel = e.Parameter as StepViewModel;
            i = stepViewModel.selectedStep.stepNumber;
            count = stepViewModel.selectedCourse.steps.Count();
            if (i == 1 && i == count)
            {
                wsteczButton.IsEnabled = false;
                dalejButton.IsEnabled = false;
            }
            else if (i == 1)
            {
                wsteczButton.IsEnabled = false;
                dalejButton.IsEnabled = true;
            }
            else if (i == count)
            {
                dalejButton.IsEnabled = false;
                wsteczButton.IsEnabled = true;
            }
            else
            {
                dalejButton.IsEnabled = true;
                wsteczButton.IsEnabled = true;
            }
            if (i>1 && stepViewModel.selectedCourse.steps.Where(s => s.stepNumber == i-1).FirstOrDefault().completed == false)
            {
                zrobione.IsEnabled = false;
            }
            if (stepViewModel.selectedStep.completed == true)
            {
                zrobione.IsEnabled = false;
                zrobione.Content = "Zrobiles to!";
            }
        }

        private void wsteczButton_Click(object sender, RoutedEventArgs e)
        {
            int a = i - 1;
            step = stepViewModel.selectedCourse.steps.Where(s => s.stepNumber == a).FirstOrDefault();
            Debug.WriteLine(step.description);
            stepViewModel.selectedStep = step;
            stepViewModel.navigateToStepViewCommand.Execute(null);
        }

        private void zrobione_Click(object sender, RoutedEventArgs e)
        {
            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbFileName);
            connection = new SQLiteConnection(new SQLitePlatformWinRT(), dbPath);
            stepViewModel.selectedStep.completed = true;
            connection.Update(stepViewModel.selectedStep);
            i = stepViewModel.selectedStep.stepNumber;
            count = stepViewModel.selectedCourse.steps.Count();
            if ((i == 1 && count == 1) || i == count)
            {
                stepViewModel.selectedCourse.completed = true;
                connection.Update(stepViewModel.selectedCourse);
                stepViewModel.navigateToCourseListViewCommand.Execute(null);
                var xmlToastTemplate = "<toast launch=\"app-defined-string\">" +
                                        "<visual>" +
                                          "<binding template =\"ToastGeneric\">" +
                                            "<text>Ukończyłeś kurs:</text>" +
                                            "<text>"+ stepViewModel.selectedCourse.name.ToString()+"</text>" +
                                            "</binding>" +
                                        "</visual>" +
                                       "</toast>";

                // load the template as XML document
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlToastTemplate);

                // create the toast notification and show to user
                var toastNotification = new ToastNotification(xmlDocument);
                var notification = ToastNotificationManager.CreateToastNotifier();
                notification.Show(toastNotification);

                var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
                int endCourses = connection.Table<CourseModel>().Where(c => c.completed == true).ToList().Count();
                int ile = connection.Table<CourseModel>().ToList().Count();
                var tileAtributes = tileXml.GetElementsByTagName("text");
                tileAtributes[0].AppendChild(tileXml.CreateTextNode("Skończyłeś: " + endCourses + "z" + ile));
                tileAtributes[1].AppendChild(tileXml.CreateTextNode("dostępnych kursów"));
                var tileNotyfication = new TileNotification(tileXml);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotyfication);

            }
            else if (i != count)
            {
                int a = i + 1;
                step = stepViewModel.selectedCourse.steps.Where(s => s.stepNumber == a).FirstOrDefault();
                stepViewModel.selectedStep = step;
                stepViewModel.navigateToStepViewCommand.Execute(null);
            
            }
            //}
            //else { 
            //  stepViewModel.selectedStep.completed = false;
            // connection.Update(stepViewModel.selectedStep);
            //}
        }

        private void dalejButton_Click(object sender, RoutedEventArgs e)
        {
            int a = i + 1;
            step = stepViewModel.selectedCourse.steps.Where(s => s.stepNumber == a).FirstOrDefault();
            stepViewModel.selectedStep = step;
            stepViewModel.navigateToStepViewCommand.Execute(null);
        }
    }
}
