using Catel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithCatel.Models
{
    public class OptionModel : ModelBase
    {

        public int Id
        {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly PropertyData IdProperty = RegisterProperty(nameof(Id), typeof(int), null);

        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string), null);



        public bool IsChecked
        {
            get { return GetValue<bool>(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly PropertyData IsCheckedProperty = RegisterProperty(nameof(IsChecked), typeof(bool), null, (sender, e) => ((OptionModel)sender).OnIsCheckedChanged());

        private void OnIsCheckedChanged()
        {
            Console.WriteLine(IsChecked);
            if (IsChecked)
            {
                OptionSelectedHandler.OnOptionSelected(Id);
            }
            
        }

        public bool IsCorrect
        {
            get { return GetValue<bool>(IsCorrectProperty); }
            set { SetValue(IsCorrectProperty, value); }
        }

        public static readonly PropertyData IsCorrectProperty = RegisterProperty(nameof(IsCorrect), typeof(bool), null);


        public IOptionSelectedHandler OptionSelectedHandler
        {
            get { return GetValue<IOptionSelectedHandler>(OptionSelectedHandlerProperty); }
            set { SetValue(OptionSelectedHandlerProperty, value); }
        }

        public static readonly PropertyData OptionSelectedHandlerProperty = RegisterProperty(nameof(OptionSelectedHandler), typeof(IOptionSelectedHandler), null);
    }
}
