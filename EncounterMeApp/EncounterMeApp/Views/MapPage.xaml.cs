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

        public List<Location> LocationList { get; set; }

        public MapPage()
        {
            InitializeComponent();

            DisplayCurrentLocation();

            LocationList = new List<Location>();
            LocationList.Add(new Location(pos: new Position(54.684384, 25.277140), point: WriteReadFile(), name: "Petro Cvirkos aikštė", own: "Tomas"));
            LocationList.Add(new Location(pos: new Position(54.685372, 25.286621), point: WriteReadFile(), name: "Katedra"));

            DisplayExistingPins();
        }

        public void DisplayExistingPins()
        {
            Location example1 = new Location(pos: new Position(54.684384, 25.277140), point: WriteReadFile(), name: "Petro Cvirkos aikštė", own: "Tomas");
            Location example2 = new Location(pos: new Position(54.685372, 25.286621), point: WriteReadFile(), name: "Katedra");
            var pin1 = new Pin()
            {
                Position = example1.position,
                Label = example1.NAME
            };
            pin1.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                string pinName = ((Pin)s).Label;
                await DisplayActionSheet(pinName, "Cancel", "Occupy", $"COORDS: {((Pin)s).Position.Latitude},{((Pin)s).Position.Longitude}",
                    $"Points: {example1.points}", $"Owner: {example1.owner}");
            };

            var pin2 = new Pin()
            {
                Position = example2.position,
                Label = example2.NAME
            };
            pin2.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                string pinName = ((Pin)s).Label;
                await DisplayActionSheet(pinName, "Cancel", "Occupy", $"COORDS: {((Pin)s).Position.Latitude},{((Pin)s).Position.Longitude}",
                    $"Points: {example2.points}", $"Owner: {example2.owner}");
            };
            mapOfVilnius.Pins.Add(pin1);
            mapOfVilnius.Pins.Add(pin2);
        }

        public async void DisplayCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            mapOfVilnius.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(5)));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("You", "Clicked", "THE BUTTON");
            var name = await App.Current.MainPage.DisplayPromptAsync("Name of your location", "Name goes here");
            var points = await App.Current.MainPage.DisplayPromptAsync("Value of your location", "Points goes here");
            var xCoord = await App.Current.MainPage.DisplayPromptAsync("X coordinate of your location", "coordinate goes here goes here");
            var yCoord = await App.Current.MainPage.DisplayPromptAsync("Y coordinate of your location", "coordinate goes here goes here");
            var position = new Position(float.Parse(xCoord), float.Parse(yCoord));
            await LocationDatabase.AddLocation(position, Int32.Parse(points), name);
            //Isvalai collection, pasiimi visus locationus is database ir juos displayini (po viena?)
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