namespace TestWithCatel.ViewModels
{
    using Catel.Data;
    using Catel.MVVM;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using TestWithCatel.Models;

    public class SliderResultsViewModel : ViewModelBase
    {

        public SliderResultsViewModel() {
            //Score = _score;
        }

        public int Score
        {
            get { return GetValue<int>(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        public static readonly PropertyData ScoreProperty = RegisterProperty(nameof(Score), typeof(int), null);
        
        public override string Title { get { return "Results"; } }

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
