namespace TestWithCatel.ViewModels
{
    using Catel.Data;
    using Catel.MVVM;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using TestWithCatel.Entity;
    using TestWithCatel.Models;

    public class MainWindowViewModel : ViewModelBase
    {
        private List<IViewModel> mSlidesList;   // список слайдов

        private IList<Question> mQuestions;     // список вопросов

        private int mScore = 0;                 // счетчик правильных ответов
        private int mScreenCounter = 0;         // счечик экранов с вопросами
        private bool isThemeSelected = false;   // индикатор выбора темы

        private IQuestionsReader questionsReader = new XMLQuestionsReader();
        // индикатор выбора ответа
        public bool IsOptionSelected
        {
            get { return GetValue<bool>(IsOptionSelectedProperty); }
            set { SetValue(IsOptionSelectedProperty, value); }
        }

        public static readonly PropertyData IsOptionSelectedProperty = RegisterProperty(nameof(IsOptionSelected), typeof(bool), null);
        // текущий экран с вопросами
        public IViewModel currentSlide
        {
            get { return GetValue<IViewModel>(currentSlideProperty); }
            set { SetValue(currentSlideProperty, value); }
        }

        public static readonly PropertyData currentSlideProperty = RegisterProperty(nameof(currentSlide), typeof(IViewModel), null);
        
        // Главное меню
        public ObservableCollection<BindableMenuItem> mainMenu
        {
            get { return GetValue<ObservableCollection<BindableMenuItem>>(mainMenuProperty); }
            set { SetValue(mainMenuProperty, value); }
        }

        public static readonly PropertyData mainMenuProperty = RegisterProperty(nameof(mainMenu), typeof(ObservableCollection<BindableMenuItem>), null);

        public MainWindowViewModel()
        {
            mSlidesList = new List<IViewModel>
            {
                new SingleAnswerSlideViewModel()
                , new SliderResultsViewModel()
            };

            StartCommand = new Command(OnStartCommandExecute);
            NextCommand = new Command(OnNextCommandExecute, () => IsOptionSelected == true);
            ExitCommand = new Command(OnExitCommandExecute);
            OpenFileCommand = new Command(OnOpenFileCommandExecute);
            SelectThemeCommand = new Command<int>(OnSelectThemeCommandExecute);
            OptionSelectCommand = new Command(OnOptionSelectCommandExecute);

            ((SingleAnswerSlideViewModel)mSlidesList[0]).IndicateAnswerCheck = OptionSelectCommand;
            // Создание меню
            mainMenu = new ObservableCollection<BindableMenuItem>();
            mainMenu.Add(
                new BindableMenuItem
                {
                    Name = "Файл",
                    Children = new ObservableCollection<BindableMenuItem> {
                    new BindableMenuItem { Name = "Открыть файл вопросов", Command = OpenFileCommand },
                    new BindableMenuItem { Name = "Выход", Command = ExitCommand} }
                });
            mainMenu.Add(new BindableMenuItem { Name = "Список тем" });
        }
        // команда запуска теста
        public Command StartCommand { get; private set; }

        private void OnStartCommandExecute()
        {
            if (isThemeSelected)
            {
                // загрузка вопросов по выбранной теме
                for (int i = 0; i < mQuestions.Count; i++)
                {
                    for (int j = 0; j < mQuestions[i].Options.Count; j++)
                    {
                        mQuestions[i].Options[j].Command = ((SingleAnswerSlideViewModel)mSlidesList[0]).SelectAnswerCommand;
                    }
                }
                //--------------------------------------
                mScreenCounter = 0;
                ((SingleAnswerSlideViewModel)mSlidesList[0]).Options.Clear();
                foreach (var item in mQuestions[0].Options)
                {
                    ((SingleAnswerSlideViewModel)mSlidesList[0]).Options.Add(item);
                }
                ((SingleAnswerSlideViewModel)mSlidesList[0]).QuestionText = mQuestions[0].Text;
                ((SingleAnswerSlideViewModel)mSlidesList[0]).QuestionNumber = mScreenCounter + 1;
                currentSlide = mSlidesList[0];
            }
        }
        // команда перехода к следующему вопросу
        public Command NextCommand { get; private set; }

        private void OnNextCommandExecute()
        {
            mScreenCounter++;

            IsOptionSelected = false;
            if (((SingleAnswerSlideViewModel)currentSlide).IsRightAnswer)
                {
                    mScore++;
                }
            if (mSlidesList.Count < mScreenCounter)
            {
                ((SliderResultsViewModel)mSlidesList[1]).Score = mScore;
                currentSlide = mSlidesList[1];
                mScore = 0;
            }
            else
            {
                ((SingleAnswerSlideViewModel)currentSlide).Options.Clear();
                foreach (var item in mQuestions[mScreenCounter].Options)
                {
                    ((SingleAnswerSlideViewModel)currentSlide).Options.Add(item);
                }
                ((SingleAnswerSlideViewModel)currentSlide).QuestionText = mQuestions[mScreenCounter].Text;
                ((SingleAnswerSlideViewModel)currentSlide).QuestionNumber = mScreenCounter + 1;
            }
        }
        // выход из программы
        public Command ExitCommand { get; private set; }

        private void OnExitCommandExecute()
        {
            App.Current.Shutdown();
        }
        // открыть файл
        public Command OpenFileCommand { get; private set; }

        private void OnOpenFileCommandExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                questionsReader.Open(openFileDialog.FileName);
                List<int> themesIdList = questionsReader.GetThemesId();
                List<string> themesList = questionsReader.GetThemes();
                // Сождаем меню с вариантами тем
                if (mainMenu[1].Children == null)
                {
                    mainMenu[1].Children = new ObservableCollection<BindableMenuItem>();
                }
                else
                {
                    mainMenu[1].Children.Clear();
                }
                for (int i = 0; i < questionsReader.GetThemesId().Count; i++)
                {
                    BindableMenuItem menuItem = new BindableMenuItem();
                    menuItem.Id = themesIdList[i];
                    menuItem.Name = themesList[i];
                    menuItem.Command = SelectThemeCommand;
                    mainMenu[1].Children.Add(menuItem);
                }
            }

        }
        // выбор темы
        public Command<int> SelectThemeCommand { get; private set; }

        private void OnSelectThemeCommandExecute(int _id)
        {
            isThemeSelected = true;
            mQuestions = questionsReader.GetQuestions(_id);
            TestName = questionsReader.GetThemeById(_id);
        }
        // реакция на выбор ответа
        public Command OptionSelectCommand { get; private set; }

        private void OnOptionSelectCommandExecute()
        {
            IsOptionSelected = true;
        }
        // тема теста
        public string TestName
        {
            get { return GetValue<string>(TestNameProperty); }
            set { SetValue(TestNameProperty, value); }
        }

        public static readonly PropertyData TestNameProperty = RegisterProperty(nameof(TestName), typeof(string), null);

        public override string Title { get { return "TestWithCatel"; } }

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
