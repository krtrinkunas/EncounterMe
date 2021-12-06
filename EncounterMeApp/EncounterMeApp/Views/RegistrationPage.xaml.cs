using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;
using EncounterMeApp.Models;
using System.Text.RegularExpressions;

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
        public async Task<Player> validateUser(Player newPlayer) //paduoti zaidejo duomenis ir tada juos pravaliduot
        {
            if (string.IsNullOrEmpty(newPlayer.NickName))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Email))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Firstname))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Lastname))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Password))
            {
                return null;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(newPlayer.Email);
            if (!match.Success)
            {
                return null;
            }
            await playerService.AddPlayer(newPlayer);
            return newPlayer; //arba return null
        }
        //playerValidation()
        //1. Sukurti zaideja, su irasytais duomenimis
        //2. Paduodu playeri i metoda, jeigu viskas ok, grazina playeri
        //3. jeigu viskas ok, as ta zaideja pridedu duombaze (metode)
        //4. Teste patikrint, ar AddPlayer buvo iskviestas, bet nereikia tikrint jo funkcionalumo
        private async void btnRegister_Clicked(object sender, EventArgs e) //
        {
            var newId = random.Next(100);
            var newPlayer = new Player { NickName = entryUserName.Text, Points = 0, Email = entryEmail.Text, Id = newId, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = entryFirstName.Text, Lastname = entryLastName.Text, Password = entryPassword.Text };
            Player validatedPlayer = await validateUser(newPlayer);

            if (validatedPlayer == null)
            {
                await DisplayAlert("Registration Failed!", "You left some required fields empty.", "OK");
            }
            else
            {
                await DisplayAlert("", "Registration Successful!", "OK");
                await Navigation.PopAsync();
            }
            /*
            //Saving registration data
            if (!string.IsNullOrEmpty(entryUserName.Text) && !string.IsNullOrEmpty(entryEmail.Text) 
                && !string.IsNullOrEmpty(entryFirstName.Text) && !string.IsNullOrEmpty(entryLastName.Text) 
                && !string.IsNullOrEmpty(entryPassword.Text))
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(entryEmail.Text);
                if (match.Success)
                {
                    var newId = random.Next(100);
                    var newPlayer = new Player { NickName = entryUserName.Text, Points = 0, Email = entryEmail.Text, Id = newId, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = entryFirstName.Text, Lastname = entryLastName.Text, Password = entryPassword.Text };
                    await playerService.AddPlayer(newPlayer);
                    await DisplayAlert("", "Registration Successful!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Registration Failed!", "Invalid email.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Registration Failed!", "You left some required fields empty.", "OK");
            }
            */
        }
    }
}