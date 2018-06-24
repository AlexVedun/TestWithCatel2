using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithCatel.Models;

namespace TestWithCatel.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public int Id_Theme { get; set; }
        public string Text { get; set; }
        public ObservableCollection<OptionModel> Options { get; set; }
        public Question()
        {
            Options = new ObservableCollection<OptionModel>();
        }
    }
}
