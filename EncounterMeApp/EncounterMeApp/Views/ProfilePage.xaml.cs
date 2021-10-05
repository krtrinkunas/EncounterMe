using EncounterMeApp.Models;
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
    public partial class ProfilePage : ContentPage
    {
        public Player newPlayer;

        public ProfilePage()
        {
            InitializeComponent();

            newPlayer = new Player { NickName = "Doomer McFeelsman", LocationsOwned = 2, LocationsVisited = 11, Points = 4856, Type = PlayerType.Creator };

            nicknameLabel.Text = newPlayer.NickName;
            pointsLabel.Text = newPlayer.Points.ToString();
            visitedPlacesLabel.Text = newPlayer.LocationsVisited.ToString();
            ownedPlacesLabel.Text = newPlayer.LocationsOwned.ToString();
        }

    }
}