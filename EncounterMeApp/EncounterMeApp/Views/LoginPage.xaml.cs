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
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            PlayerList = new List<Player>();

            PlayerList.Clear();

            var players = await PlayerDatabase.GetPlayers();

            PlayerList.AddRange(players);

            /*if (Application.Current.Properties.ContainsKey("Username") && Application.Current.Properties.ContainsKey("Password"))
            {
                var username = Application.Current.Properties["Username"] as string;
                var password = Application.Current.Properties["Password"] as string;
                var firstname = Application.Current.Properties["Firstname"] as string;

                if (entryUserName.Text == username && entryPassword.Text == password)
                {
                    DisplayAlert("Login Successful!", "Welcome " + firstname + "!", "OK");
                }
                else
                {
                    DisplayAlert("", "Login Failed!", "OK");
                }
            }
            else
            {
                DisplayAlert("", "Login Failed!", "OK");
            }
            */

            foreach (Player player in PlayerList)
            {
                if (player.NickName == entryUserName.Text)
                {
                    if (player.Password == entryPassword.Text)
                    {
                        await DisplayAlert("Login Successful!", "Welcome " + player.Firstname + "!", "OK");
                        App.player = player;
                        //var Detail = new NavigationPage(new ProfilePage(player));

                        await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
                        //Application.Current.MainPage  = new AppShell();
                        //await Navigation.PopAsync(new AppShell());
                    }
                }
            }
        }
    }
}