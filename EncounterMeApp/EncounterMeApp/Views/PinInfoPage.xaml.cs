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

        private async void Remove_Button_Clicked(object sender, EventArgs e)
        {
            if(App.player.NickName == currentLocation.owner)
            {
                bool action = await DisplayAlert(currentLocation.NAME, "Are you sure you want to remove this location?", "Yes", "Cancel");
                if(action)
                {
                    App.player.LocationsOwned -= 1;
                    await playerService.UpdatePlayer(App.player);

                    await locationService.DeleteLocation(currentLocation);

                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                await DisplayAlert("Forbidden action", "Only location owners can remove locations", "Got it");
            }
        }
    }
}