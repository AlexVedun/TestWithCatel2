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
    // класс-модель, упрощенно описывающий элемент меню
    public class BindableMenuItem : ModelBase
    {
        // идентификатор
        public int Id
        {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly PropertyData IdProperty = RegisterProperty(nameof(Id), typeof(int), null);
        // текст пункта меню
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string), null);
        // коллекция элементов подменю
        public ObservableCollection<BindableMenuItem> Children
        {
            get { return GetValue<ObservableCollection<BindableMenuItem>>(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        public static readonly PropertyData ChildrenProperty = RegisterProperty(nameof(Children), typeof(ObservableCollection<BindableMenuItem>), null);
        // команда, которая выполняется по клику на пункт меню
        public ICommand Command
        {
            get { return GetValue<ICommand>(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly PropertyData CommandProperty = RegisterProperty(nameof(Command), typeof(ICommand), null);
    }
}
