using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApp.Models
{
    public class CourseModel : INotifyPropertyChanged
    {   
        [PrimaryKey]
        public int courseId { get; set; }
        private string _name;
        private ObservableCollection<StepModel> _steps;
        private bool _completed;

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        [Ignore]
        public ObservableCollection<StepModel> steps
        {
            get
            {
                //_steps = new ObservableCollection<StepModel>();
                return _steps;
            }
            set
            {
                _steps = value;
            }
        }

        public bool completed
        {
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
