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
        public static Lazy<Task<List<MyLocation>>> _LocationList = null;
        public static Lazy<Task<List<MyLocation>>> _MyLocationList = null;

        /*public List<MyLocation> LocationList
        {
            get 
            {
                return _LocationList.Value; 
            }
        }*/
        public MapPage()
        {
            locationService = DependencyService.Get<ILocationService>();
            InitializeComponent();
            DisplayCurrentLocation();


            _LocationList = new Lazy<Task<List<MyLocation>>>(async () => await LoadLocations());
            _MyLocationList = new Lazy<Task<List<MyLocation>>>(async () => await LoadMyLocations());
            //DisplayExistingPins();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //DisplayExistingPins();
        }
        public async void DisplayExistingPins(Lazy<Task<List<MyLocation>>> list)
        {
            //LocationList.Clear();
            //var locations = await locationService.GetLocations();

            //LocationList.AddRange(locations);
            //var list = await _LocationList.Value;
            mapOfVilnius.Pins.Clear();
            foreach (MyLocation location in await list.Value)
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
                    string action = await DisplayActionSheet(pinName, "Cancel", "Occupy", $"COORDS: {((Pin)s).Position.Latitude},{((Pin)s).Position.Longitude}",
                        $"Points: {location.points}", $"Owner: {location.owner}", "More info");
                    if (action == "More info")
                    {
                        _ = Navigation.PushAsync(new PinInfoPage(pinName, location.owner, location.points));
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

        public async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewLocationPage());
            mapOfVilnius.Pins.Clear();
            _LocationList = new Lazy<Task<List<MyLocation>>>(async () => await LoadLocations());
            _MyLocationList = new Lazy<Task<List<MyLocation>>>(async () => await LoadMyLocations());
            //DisplayExistingPins();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            DisplayExistingPins(_LocationList);
        }
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            DisplayExistingPins(_MyLocationList);
        }
        private void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            mapOfVilnius.Pins.Clear();
        }

        public async Task<List<MyLocation>> LoadLocations()
        {
            List<MyLocation> temp = new List<MyLocation>();
            var locations = await locationService.GetLocations();

            temp.AddRange(locations);

            return temp;
        }
        public async Task<List<MyLocation>> LoadMyLocations()
        {
            List<MyLocation> temp = new List<MyLocation>();
            var locations = await locationService.GetLocations();


            IEnumerable<MyLocation> myLocations =
                from location in locations
                where location.owner == App.player.NickName
                select location;
            temp.AddRange(myLocations);

            return temp;
        }
    }
}