using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EncounterMeApp.ViewModels
{
    public class LeaderboardViewModel : BaseViewModel //Imported from MVVM helpers
    {
        public LeaderboardViewModel()
        {
            IncreaseCount = new Command(OnIncrease); //Imported from MVVM helpers
            Title = "Leaderboad"; //From MVVM helper
        }
        public ICommand IncreaseCount { get; }

        int count = 0;
        string countDisplay = "Click Me";
        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value); //From MVVM helper
        }

        void OnIncrease()
        {
            count++;
            CountDisplay = $"You clicked {count} time(s)";
        }
    }
}
