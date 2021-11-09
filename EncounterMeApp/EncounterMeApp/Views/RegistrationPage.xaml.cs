using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;
using EncounterMeApp.Models;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        IPlayerService playerService;
        public RegistrationPage()
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
        }

        Random random = new Random();
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //Saving registration data
            await DisplayAlert("","Registration Successful!", "OK");
            var newId = random.Next(100);
            var newPlayer = new Player { NickName = entryUserName.Text, Points = 0, Email = entryEmail.Text, Id = newId, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = entryFirstName.Text, Lastname = entryLastName.Text, Password = entryPassword.Text };
            await playerService.AddPlayer(newPlayer);
            //await PlayerDatabase.AddPlayer(entryUserName.Text, 0, entryFirstName.Text, entryLastName.Text, entryPassword.Text, entryEmail.Text);
            await Navigation.PopAsync();
        }
    }
}