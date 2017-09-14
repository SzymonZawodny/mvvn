using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEventsApp.Helpers
{
    public interface INavigationHelper
    {
        void NavigateTo(Type pageType, object parameter);
        void NavigateTo(Type pageType);

        void GoBack();
    }
}
