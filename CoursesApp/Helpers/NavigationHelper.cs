using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameEventsApp.Helpers
{
    class NavigationHelper : INavigationHelper
    {
        private Frame frame;

        public NavigationHelper(Frame frame)
        {
            if (frame == null) throw new ArgumentNullException();

            this.frame = frame;
        }
        public void GoBack()
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        public void NavigateTo(Type pageType, object parameter)
        {
            frame.Navigate(pageType, parameter);
        }

        public void NavigateTo(Type pageType)
        {
            frame.Navigate(pageType);
        }
    }
}
