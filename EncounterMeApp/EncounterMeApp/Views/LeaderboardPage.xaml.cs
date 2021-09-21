using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderboardPage : ContentPage
    {
        public LeaderboardPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        int count = 0;
        string countDisplay = "Click Me";
        public string CountDisplay
        {
            get => countDisplay;
            set
            {
                if (value == countDisplay)
                {
                    return;
                }

                countDisplay = value;
                OnPropertyChanged();
            }
        }

        private void ButtonClick_Clicked(object sender, EventArgs e)
        {
            count++;
            CountDisplay = $"You clicked {count} time(s)";
        }
    }
}