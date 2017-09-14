using CoursesApp.ViewModels;
using GameEventsApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class CourseListView : Page
    {
        CoursesListViewModel coursesListViewModel { get; set; }
        Frame frame = Window.Current.Content as Frame;
        public CourseListView()
        {
            this.InitializeComponent();
            NavigationHelper navigationHelper = new NavigationHelper(frame);
            coursesListViewModel = new CoursesListViewModel(navigationHelper);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            coursesListViewModel.getCoursesCommand.Execute(null);
            
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            coursesListViewModel.selectedCourse = e.ClickedItem as Models.CourseModel;
            coursesListViewModel.navigateToStepListViewCommand.Execute(null);
        }
    }
}
