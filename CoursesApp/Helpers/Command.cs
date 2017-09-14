using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameEventsApp.Helpers
{
    public class Command : ICommand
    {
        private Action<object> action;
        private Func<object, bool> canExecuteFunctor;

        public event EventHandler CanExecuteChanged;

        public Command(Action<object> action, Func<object, bool> canExecuteFunctor = null)
        {
            if (action == null) throw new ArgumentNullException();

            this.action = action;
            this.canExecuteFunctor = canExecuteFunctor;
        }

        public bool CanExecute(object parameter)
        {
            bool canCommandExecute = true;

            if (canExecuteFunctor != null)
            {
                canCommandExecute = canExecuteFunctor(parameter);
            }

            return canCommandExecute;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                action(parameter);
            }
        }
    }
}
