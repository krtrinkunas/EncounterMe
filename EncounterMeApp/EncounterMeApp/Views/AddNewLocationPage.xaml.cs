using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Models;
using EncounterMeApp.Services;
using EncounterMeApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewLocationPage : ContentPage
    {
        //public InternetLocationService service = new InternetLocationService();
        ILocationService locationService;
        IPlayerService playerService;
        public AddNewLocationPage()
        {
            InitializeComponent();
            DisplayCurrentLocation();

            locationService = DependencyService.Get<ILocationService>();
            playerService = DependencyService.Get<IPlayerService>();
        }

        //public List<MyLocation> tempList = new List<MyLocation>();
        private async void MyMap_MapClickedAsync(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            var xCoord = e.Position.Latitude;
            var yCoord = e.Position.Longitude;
            string action = await DisplayActionSheet("Do you want to add a location in these coordinates?", "Cancel", "Yes", $"X: {xCoord}", $"Y: {yCoord}");
            if (action == "Yes")
            {
                string name;
                do
                {
                    name = await App.Current.MainPage.DisplayPromptAsync("Name of your location", "Name goes here");
                }
                while (name == "");

                string points;
                do
                {
                    points = await App.Current.MainPage.DisplayPromptAsync("Value of your location", "Points goes here");
                }
                while (points == "");

                Random random = new Random();
                var newId = random.Next(100);
                var newLocation = new MyLocation{ NAME = name, points = Int32.Parse(points), positionX = xCoord, positionY = yCoord, owner = App.player.NickName, Id = newId};

                App.player.LocationsOwned += 1;
                await playerService.UpdatePlayer(App.player);

                await locationService.AddLocation(newLocation);

                
            }
            await Navigation.PopToRootAsync();
        }
        public async void DisplayCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(5)));
        }
    }
}