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

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        //private readonly Geocoder _geocoder = new Geocoder();
        string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "points.txt");
        public struct Location
        {
            public Location(Position pos, int point, string name, string own = "No owner")
            {
                position = pos;
                NAME = name;
                points = point;
                owner = own;
            }

            public Position position { get; }
            public string NAME { get; }
            public int points { get; }
            public string owner { get; }
        }
        public MapPage()
        {
            InitializeComponent();
            DisplayCurrentLocation();
            DisplayExistingPins();
        }

        public void DisplayExistingPins()
        {
            Location a = new Location(new Position(54.684384, 25.277140), WriteReadFile(), "Petro Cvirkos aikštė");
            var pin1 = new Pin()
            {
                Position = a.position,
                Label = a.NAME
            };
            pin1.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                string pinName = ((Pin)s).Label;
                await DisplayActionSheet(pinName, "Cancel", "Occupy", $"COORDS: {((Pin)s).Position.Latitude},{((Pin)s).Position.Longitude}",
                    $"Points: {a.points}", $"Owner: {a.owner}");
            };

            var pin2 = new Pin()
            {
                Position = new Position(54.685372, 25.286621),
                Label = "Katedra"
            };
            mapOfVilnius.Pins.Add(pin1);
            mapOfVilnius.Pins.Add(pin2);
        }

        public async void DisplayCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            mapOfVilnius.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(5)));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("You", "Clicked", "THE BUTTON");
        }
        public int WriteReadFile()
        {
            File.WriteAllText(file, "1234");
            string pointsStr = File.ReadAllText(file);

            try
            {
                int result = Int32.Parse(pointsStr);
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{pointsStr}'");
                return 0;
            }
        }
    }
}