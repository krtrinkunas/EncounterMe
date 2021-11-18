using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;
using EncounterMeApp.Models;
using EncounterMeApp;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public List<Player> PlayerList;
        public Player CurrentPlayer;
        IPlayerService playerService;
        public LoginPage()
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(entryUserName.Text) && !string.IsNullOrEmpty(entryPassword.Text))
            { 
                PlayerList = new List<Player>();

                PlayerList.Clear();

                var players = await playerService.GetPlayers();

                PlayerList.AddRange(players);

                Player newPlayer = PlayerList.Find(delegate (Player play)
                {
                    return play.NickName == entryUserName.Text && play.Password == entryPassword.Text;
                });

                if (newPlayer != null)
                {
                    await DisplayAlert("Login Successful!", "Welcome " + newPlayer.Firstname + "!", "OK");
                    App.player = newPlayer;
                    await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
                }
                else
                {
                    await DisplayAlert("Login Failed!", "This account does not exist", "OK");
                }
            }
            else
            {
                await DisplayAlert("Login Failed!", "You left your username or password empty", "OK");
            }
        }
    }
}
