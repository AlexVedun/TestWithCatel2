using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWithCatel.ViewModels
{
    public class BindableMenuItem : INotifyPropertyChanged
    {
        private string _name;
        private List<BindableMenuItem> _children;
        private ICommand _command;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public List<BindableMenuItem> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand Command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
