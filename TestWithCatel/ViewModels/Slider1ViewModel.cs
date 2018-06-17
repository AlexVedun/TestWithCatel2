namespace TestWithCatel.ViewModels
{
    using Catel.Data;
    using Catel.MVVM;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using TestWithCatel.Models;

    public class Slider1ViewModel : ViewModelBase
    {
        public bool RightAnswer
        {
            get { return GetValue<bool>(RightAnswerProperty); }
            set { SetValue(RightAnswerProperty, value); }
        }

        public static readonly PropertyData RightAnswerProperty =
            RegisterProperty(nameof(RightAnswer), typeof(bool), null);

        public Slider1ViewModel()
        {
            Options = new ObservableCollection<OptionModel>();
            /*foreach (var item in _options)
            {
                Options.Add(item);
            }*/
        }

        public ObservableCollection<OptionModel> Options
        {
            get { return GetValue<ObservableCollection<OptionModel>>(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public static readonly PropertyData OptionsProperty =
            RegisterProperty(nameof(Options), typeof(ObservableCollection<OptionModel>), null);

        public OptionModel SelectedOption
        {
            get { return GetValue<OptionModel>(SelectedOptionProperty); }
            set { SetValue(SelectedOptionProperty, value); }
        }

        public static readonly PropertyData SelectedOptionProperty =
            RegisterProperty(nameof(SelectedOption), typeof(OptionModel), null);


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
