using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithCatel.Models;

namespace TestWithCatel.Entity
{
    // класс, описывающий вопрос с вариантами ответов
    public class Question
    {
        public int Id { get; set; }                                     // идентификатор вопроса
        public int Id_Theme { get; set; }                               // идентификатор темы
        public string Text { get; set; }                                // текст вопроса
        public ObservableCollection<OptionModel> Options { get; set; }  // варианты ответов

        public Question()
        {
            Options = new ObservableCollection<OptionModel>();
        }
    }
}
