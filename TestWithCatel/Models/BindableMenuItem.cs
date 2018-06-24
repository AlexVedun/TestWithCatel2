using Catel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWithCatel.Models
{
    public class BindableMenuItem : ModelBase//INotifyPropertyChanged
    {
        /*private string _name;
        private List<BindableMenuItem> _children;
        private ICommand _command;*/


        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string), null);


        public ObservableCollection<BindableMenuItem> Children
        {
            get { return GetValue<ObservableCollection<BindableMenuItem>>(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        public static readonly PropertyData ChildrenProperty = RegisterProperty(nameof(Children), typeof(ObservableCollection<BindableMenuItem>), null);


        public ICommand Command
        {
            get { return GetValue<ICommand>(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly PropertyData CommandProperty = RegisterProperty(nameof(Command), typeof(ICommand), null);

        /*public string Name
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
        }*/
    }
}
