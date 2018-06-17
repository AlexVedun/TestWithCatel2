namespace TestWithCatel.ViewModels
{
    using Catel.Data;
    using Catel.MVVM;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using TestWithCatel.Entity;
    using TestWithCatel.Models;

    public class MainWindowViewModel : ViewModelBase, IOptionSelectedHandler
    {
        private List<IViewModel> mSlidesList;
        //private bool mIsOptionSelected = false;

        private ObservableCollection<OptionModel> mOptionsMock;
        private ObservableCollection<OptionModel> mOptionsMock2;

        private IList<Question> mQuestions;

        private int mScore = 0;
        private int mScreenCounter = 0;

        public bool IsOptionSelected
        {
            get { return GetValue<bool>(IsOptionSelectedProperty); }
            set { SetValue(IsOptionSelectedProperty, value); }
        }

        public static readonly PropertyData IsOptionSelectedProperty = RegisterProperty(nameof(IsOptionSelected), typeof(bool), null);

        public IViewModel currentSlide
        {
            get { return GetValue<IViewModel>(currentSlideProperty); }
            set { SetValue(currentSlideProperty, value); }
        }

        public static readonly PropertyData currentSlideProperty = RegisterProperty(nameof(currentSlide), typeof(IViewModel), null);

        public MainWindowViewModel()
        {
            mOptionsMock =
               new ObservableCollection<OptionModel> {

                        new OptionModel(){ Id = 1, Name = "Перлит", IsCorrect = false, OptionSelectedHandler = this }
                        , new OptionModel(){ Id = 2, Name = "Феррит", IsCorrect = false, OptionSelectedHandler = this }
                        , new OptionModel(){ Id = 3, Name = "Цементит", IsCorrect = true, OptionSelectedHandler = this }
                        , new OptionModel(){ Id = 4, Name = "Аустенит", IsCorrect = false, OptionSelectedHandler = this }
                   };

            mOptionsMock2 =
               new ObservableCollection<OptionModel> {

                        new OptionModel(){ Id = 5, Name = "1", IsCorrect = true, OptionSelectedHandler = this }
                        , new OptionModel(){ Id = 6, Name = "2", IsCorrect = false, OptionSelectedHandler = this }
                        , new OptionModel(){ Id = 7, Name = "3", IsCorrect = false, OptionSelectedHandler = this }
                        , new OptionModel(){ Id = 8, Name = "4", IsCorrect = false, OptionSelectedHandler = this }
                   };

            mQuestions = new List<Question>() {

                new Question(){
                    Id = 1
                    , Text = "Укажите структурную составляющую с наибольшей твердостью:"
                    , Options = mOptionsMock}
                , new Question(){
                    Id = 2
                    , Text = "Сколько?"
                    , Options = mOptionsMock2}
            };

            mSlidesList = new List<IViewModel>
            {
                //new Slider1ViewModel(mOptionsMock)
                new Slider1ViewModel()
                , new SliderResultsViewModel()
            };
            //currentSlide = mSlidesList[0];
            // TODO: Move code below to constructor
            StartCommand = new Command(OnStartCommandExecute);

            Console.WriteLine(IsOptionSelected);
            NextCommand = new Command(OnNextCommandExecute, () => IsOptionSelected == true);
        }

        public Command StartCommand { get; private set; }

        private void OnStartCommandExecute()
        {
            // TODO: Handle command logic here
            mScreenCounter = 0;
            ((Slider1ViewModel)mSlidesList[0]).Options.Clear();
            foreach (var item in mQuestions[0].Options)
            {
                ((Slider1ViewModel)mSlidesList[0]).Options.Add(item);
            }
            ((Slider1ViewModel)mSlidesList[0]).QuestionText = mQuestions[0].Text;
            ((Slider1ViewModel)mSlidesList[0]).QuestionNumber = mScreenCounter + 1;
            currentSlide = mSlidesList[0];
        }

        public Command NextCommand { get; private set; }

        private void OnNextCommandExecute()
        {
            mScreenCounter++;

            IsOptionSelected = false;
            foreach (var item in ((Slider1ViewModel)currentSlide).Options)
            {
                if (item.IsChecked == true && item.IsCorrect == true)
                {
                    Console.WriteLine("You're right!");
                    mScore++;
                }
                item.IsChecked = false;
            }
            Console.WriteLine(mSlidesList.Count + " ");
            if (mSlidesList.Count == mScreenCounter)
            {
                Console.WriteLine("The end");
                //mSlidesList.Add(new SliderResultsViewModel(mScore));
                ((SliderResultsViewModel)mSlidesList[1]).Score = mScore;
                currentSlide = mSlidesList[1];
                mScore = 0;
            }
            else {
                ((Slider1ViewModel)currentSlide).Options.Clear();
                foreach (var item in mQuestions[mScreenCounter].Options)
                {
                    ((Slider1ViewModel)currentSlide).Options.Add(item);
                }
                ((Slider1ViewModel)currentSlide).QuestionText = mQuestions[mScreenCounter].Text;
                ((Slider1ViewModel)currentSlide).QuestionNumber = mScreenCounter + 1;
            }

            
        }

        public override string Title { get { return "TestWithCatel"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        public void OnOptionSelected(int _id) {

            IsOptionSelected = true;
            Console.WriteLine(_id);
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
