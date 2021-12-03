using EncounterMeApp.Models;
using EncounterMeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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

        private async void Occupy_Button_Clicked(object sender, EventArgs e)
        {
            if (App.player.NickName != currentLocation.owner)
            {
                var currentCoords = await Geolocation.GetLastKnownLocationAsync();
                if (Location.CalculateDistance(currentLocation.positionX, currentLocation.positionY, currentCoords.Latitude, currentCoords.Longitude, 0) <= 1)
                {
                    string answer = await DisplayPromptAsync("2 + 2 = ", "Answer goes here");
                    if (answer == "4")
                    {
                        App.player.Points += currentLocation.points;
                        App.player.LocationsOwned += 1;
                        await playerService.UpdatePlayer(App.player);
                        ownerOfPin.Text = "Owner: " + App.player.NickName;

                        currentLocation.owner = App.player.NickName;
                        await locationService.UpdateLocation(currentLocation);
                    }
                }
                else
                {
                    await DisplayAlert("Forbidden action", "To try to occupy a location, you must be near it", "Got it");
                }
            }
            else
            {
                await DisplayAlert("Forbidden action", "You can't occupy your own location", "Got it");
            }
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