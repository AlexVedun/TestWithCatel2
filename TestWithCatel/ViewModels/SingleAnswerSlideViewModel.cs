using Catel.Data;
using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestWithCatel.Models;

namespace TestWithCatel.ViewModels
{
    class SingleAnswerSlideViewModel: ViewModelBase
    {
        public SingleAnswerSlideViewModel()
        {
            Options = new ObservableCollection<OptionModel>();
            SelectAnswerCommand = new Command<int>(OnSelectAnswerCommandExecute);
        }

        public ObservableCollection<OptionModel> Options
        {
            get { return GetValue<ObservableCollection<OptionModel>>(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public static readonly PropertyData OptionsProperty = RegisterProperty(nameof(Options), typeof(ObservableCollection<OptionModel>), null);

        public bool IsRightAnswer
        {
            get { return GetValue<bool>(IsRightAnswerProperty); }
            set { SetValue(IsRightAnswerProperty, value); }
        }

        public static readonly PropertyData IsRightAnswerProperty = RegisterProperty(nameof(IsRightAnswer), typeof(bool), null);

        public OptionModel SelectedOption
        {
            get { return GetValue<OptionModel>(SelectedOptionProperty); }
            set { SetValue(SelectedOptionProperty, value); }
        }

        public static readonly PropertyData SelectedOptionProperty = RegisterProperty(nameof(SelectedOption), typeof(OptionModel), null);

        public string QuestionText
        {
            get { return GetValue<string>(QuestionTextProperty); }
            set { SetValue(QuestionTextProperty, value); }
        }

        public static readonly PropertyData QuestionTextProperty = RegisterProperty(nameof(QuestionText), typeof(string), null);

        public int QuestionNumber
        {
            get { return GetValue<int>(QuestionNumberProperty); }
            set { SetValue(QuestionNumberProperty, value); }
        }

        public static readonly PropertyData QuestionNumberProperty = RegisterProperty(nameof(QuestionNumber), typeof(int), null);

        public bool IsAnswerChecked
        {
            get { return GetValue<bool>(IsAnswerCheckedProperty); }
            set { SetValue(IsAnswerCheckedProperty, value); }
        }

        public static readonly PropertyData IsAnswerCheckedProperty = RegisterProperty(nameof(IsAnswerChecked), typeof(bool), null);

        public Command<int> SelectAnswerCommand { get; private set; }

        private void OnSelectAnswerCommandExecute(int _id)
        {
            var answer = Options.Where(e => ((int)e.Id == _id)).FirstOrDefault();
            if (answer.IsCorrect)
            {
                IsRightAnswer = true;
            }
            else
            {
                IsRightAnswer = false;
            }
            IsAnswerChecked = true;
            IndicateAnswerCheck.Execute();
        }

        public Command IndicateAnswerCheck
        {
            get { return GetValue<Command>(IndicateAnswerCheckProperty); }
            set { SetValue(IndicateAnswerCheckProperty, value); }
        }

        public static readonly PropertyData IndicateAnswerCheckProperty = RegisterProperty(nameof(IndicateAnswerCheck), typeof(Command), null);

        public override string Title { get { return "View model title"; } }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
        }
    }
}
