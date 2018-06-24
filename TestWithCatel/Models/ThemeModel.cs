using Catel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithCatel.Models
{
    public class ThemeModel : ModelBase
    {

        public int Id
        {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly PropertyData IdProperty = RegisterProperty(nameof(Id), typeof(int), null);


        public string Text
        {
            get { return GetValue<string>(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly PropertyData TextProperty = RegisterProperty(nameof(Text), typeof(string), null);
    }
}
