using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoursesApp.Models
{
    public class StepModel : INotifyPropertyChanged
    {
        [PrimaryKey]
        public int stepId { get; set; }
        private int _courseId;
        private int _stepNumber;
        private string _description;
        private bool _completed;

        public int courseId
        {
            get
            {
                return _courseId;
            }
            set
            {
                _courseId = value;
            }
        }

        public int stepNumber
        {
            get
            {
                return _stepNumber;
            }
            set
            {
                _stepNumber = value;
            }
        }

        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
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
