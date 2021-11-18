using EncounterMeApp.Models;
using EncounterMeApp.Services;
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
    public partial class PinInfoPage : ContentPage
    {

        MyLocation currentLocation;
        IPlayerService playerService;
        ILocationService locationService;
        public PinInfoPage(MyLocation location)
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
            locationService = DependencyService.Get<ILocationService>();

            nameOfPin.Text = location.NAME;
            ownerOfPin.Text = "Owner: " + location.owner;
            pointsOfPin.Text = location.points.ToString();

            currentLocation = location;
        }

        private void Occupy_Button_Clicked(object sender, EventArgs e)
        {
            App.player.Points += currentLocation.points;
            App.player.LocationsOwned += 1;
            playerService.UpdatePlayer(App.player);
            ownerOfPin.Text = "Owner" + App.player.NickName;

            currentLocation.owner = App.player.NickName;
            locationService.UpdateLocation(currentLocation);
        }
    }
}