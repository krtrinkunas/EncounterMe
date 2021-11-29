using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using EncounterMeApp.Services;
using EncounterMeApp.Models;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        ILocationService locationService;
        IPlayerService playerService;
        public List<MyLocation> _LocationList = null;

        public MapPage()
        {
            locationService = DependencyService.Get<ILocationService>();
            playerService = DependencyService.Get<IPlayerService>();
            InitializeComponent();
            DisplayCurrentLocation();


            _LocationList = new List<MyLocation>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public void DisplayExistingPins(List<MyLocation> list)
        {
            mapOfVilnius.Pins.Clear();
            foreach (MyLocation location in list)
            {
                var pin = new Pin()
                {
                    Position = new Position(location.positionX, location.positionY),
                    Label = location.NAME
                };
                pin.MarkerClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    string pinName = ((Pin)s).Label;
                    string action = await DisplayActionSheet(pinName, "OK", "Cancel", $"COORDS: {((Pin)s).Position.Latitude},{((Pin)s).Position.Longitude}", //Need to remove the 'OK' button
                        $"Points: {location.points}", $"Owner: {location.owner}", "More info");
                    if (action == "More info")
                    {
                        App.player.LocationsVisited += 1;
                        await playerService.UpdatePlayer(App.player);
                        _ = Navigation.PushAsync(new PinInfoPage(location));
                    }
                };
                mapOfVilnius.Pins.Add(pin);
            }
        }

        public async void DisplayCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            mapOfVilnius.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(5)));
        }

        public async void Button_Clicked_Create_Location(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewLocationPage());
            mapOfVilnius.Pins.Clear();
        }

        private async void Show_All_Locations_Clicked(object sender, EventArgs e)
        {
            await LoadLocations();
            DisplayExistingPins(_LocationList);
        }
        private async void Show_My_Locations_Clicked(object sender, EventArgs e)
        {
            await LoadMyLocations();
            DisplayExistingPins(_LocationList);
        }
        public async Task LoadLocations()
        {
            List<MyLocation> temp = new List<MyLocation>();
            var locations = await locationService.GetLocations();

            temp.AddRange(locations);
            
            _LocationList = temp;
        }
        public async Task LoadMyLocations()
        {
            List<MyLocation> temp = new List<MyLocation>();
            var locations = await locationService.GetLocations();


            IEnumerable<MyLocation> myLocations =
                from location in locations
                where location.owner == App.player.NickName
                select location;
            temp.AddRange(myLocations);

            _LocationList = temp;
        }
    }
}