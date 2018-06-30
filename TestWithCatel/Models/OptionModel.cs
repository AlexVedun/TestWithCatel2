using Catel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestWithCatel.Models
{
    // класс-модель, описывающий вариант ответа на вопрос
    public class OptionModel : ModelBase
    {
        // идентификатор
        public int Id
        {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly PropertyData IdProperty = RegisterProperty(nameof(Id), typeof(int), null);
        // текст ответа
        public string Name
        {
            get { return GetValue<string>(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly PropertyData NameProperty = RegisterProperty(nameof(Name), typeof(string), null);
        // команда, выполняемая при выборе ответа
        public ICommand Command
        {
            get { return GetValue<ICommand>(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly PropertyData CommandProperty = RegisterProperty(nameof(Command), typeof(ICommand), null);
        // свойство, обозначающее правильный ответ или нет
        public bool IsCorrect
        {
            get { return GetValue<bool>(IsCorrectProperty); }
            set { SetValue(IsCorrectProperty, value); }
        }

        public static readonly PropertyData IsCorrectProperty = RegisterProperty(nameof(IsCorrect), typeof(bool), null);
    }
}
