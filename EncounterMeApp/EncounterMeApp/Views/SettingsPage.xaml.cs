using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Text.RegularExpressions;
using EncounterMeApp.Services;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {

        IPlayerService playerService;
        public SettingsPage()
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
            ThemeSwitch.IsToggled = Preferences.Get("ThemeSwitch", false);
        }

        private void Save_Button_Clicked(object sender, EventArgs e) //To define, what needs to be saved
        {
            Preferences.Set("ThemeSwitch", ThemeSwitch.IsToggled);
        }

        private async void LogOut_Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Are you sure you want to Log out?", "Think twice", "OK");
        }
        private void Reset_Button_Clicked(object sender, EventArgs e)//To clear, what has been saved
        {
            Preferences.Clear();
        }
        private async void ChangeEmail_Button_Clicked(object sender, EventArgs e)
        {
            //Regex for email
            var newEmail = UserInput.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(newEmail);
            if (match.Success)
            {
                App.player.Email = newEmail;
                await playerService.UpdatePlayer(App.player);
                await DisplayAlert("Success!", $"Your new email is: {newEmail}", "OK");
            }
            else
            {
                await DisplayAlert("Error!", "Email invalid.", "OK");
            }  
        }
    }
}