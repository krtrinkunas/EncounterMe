using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Views;
using EncounterMeApp.Services;
using Xamarin.Essentials;

namespace EncounterMeApp.Views
{
    //[QueryProperty "username"]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public string username;
        public Player newPlayer;
        public List<Player> PlayerList;
        public ProfilePage()
        {
            //NavigationPage.SetBackButtonTitle(this, "Back");
            InitializeComponent();
            //newPlayer = new Player { NickName = "Doomer McFeelsman", LocationsOwned = 2, LocationsVisited = 11, Points = 4856, Type = PlayerType.Creator, Email = "pepe@pepe.com" };
            ProfileImage.Source = ImageSource.FromFile("Peter");
            nicknameLabel.Text = App.player.NickName;
            pointsLabel.Text = App.player.Points.ToString();
            visitedPlacesLabel.Text = App.player.LocationsVisited.ToString();
            ownedPlacesLabel.Text = App.player.LocationsOwned.ToString();
            currentEmail.Text = App.player.Email;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            nicknameLabel.Text = App.player.NickName;
            pointsLabel.Text = App.player.Points.ToString();
            visitedPlacesLabel.Text = App.player.LocationsVisited.ToString();
            ownedPlacesLabel.Text = App.player.LocationsOwned.ToString();
            currentEmail.Text = App.player.Email;
        }

        private async void btnProfilePicture_Clicked(object sender, EventArgs e)
        {
            string profilePicPath = await DisplayPromptAsync("What is the file name?", "(The file must exist in Resources/drawable folder of the app", keyboard: Keyboard.Default);
            ProfileImage.Source = profilePicPath;

            //var result = MediaPicker.PickPhotoAsync(new MediaPickerOptions
            //{
            //    Title = "Please pick a photo!"
            //});

            //var stream = await result.OpenReadAsync();

        }

        /*public ProfilePage()
{
   InitializeComponent();
   newPlayer = new Player { NickName = "Doomer McFeelsman", LocationsOwned = 2, LocationsVisited = 11, Points = 4856, Type = PlayerType.Creator, Email = "pepe@pepe.com" };
   nicknameLabel.Text = newPlayer.NickName;
   pointsLabel.Text = newPlayer.Points.ToString();
   visitedPlacesLabel.Text = newPlayer.Lastname;
   ownedPlacesLabel.Text = newPlayer.Firstname;
   currentEmail.Text = newPlayer.Email;
}*/

        /*public ProfilePage(string username)
        {
            InitializeComponent();

            PlayerList = new List<Player>();

            PlayerList.Clear();

            var players = PlayerDatabase.GetPlayers();

            PlayerList.AddRange((IEnumerable<Player>)players);

            foreach(Player player in PlayerList)
            {
                if (username == player.NickName)
                {
                    nicknameLabel.Text = App.UserName;
                    pointsLabel.Text = player.Points.ToString();
                    visitedPlacesLabel.Text = player.Lastname;
                    ownedPlacesLabel.Text = player.Firstname;
                    currentEmail.Text = player.Email;
                }
            }
            

        }*/

    }
}