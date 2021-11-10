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
        private Lazy<List<MyLocation>> _LocationList = null;

        public List<MyLocation> LocationList
        {
            get 
            {
                return _LocationList.Value; // CIA LUZTA
            }
        }
        public MapPage()
        {
            locationService = DependencyService.Get<ILocationService>();
            InitializeComponent();
            DisplayCurrentLocation();

            _LocationList = new Lazy<List<MyLocation>>(() => LoadLocations().Result);
            //DisplayExistingPins();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //DisplayExistingPins();
        }
        public void DisplayExistingPins()
        {
            //LocationList.Clear();
            //var locations = await locationService.GetLocations();

            //LocationList.AddRange(locations);

            foreach (MyLocation location in LocationList)
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

            DisplayExistingPins();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            DisplayExistingPins();
        }
        public async Task<List<MyLocation>> LoadLocations()
        {
            List<MyLocation> temp = new List<MyLocation>();
            var locations = await locationService.GetLocations();

            temp.AddRange(locations);

            return temp;
        }
    }
}