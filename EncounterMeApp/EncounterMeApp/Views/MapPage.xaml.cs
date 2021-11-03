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
        //private readonly Geocoder _geocoder = new Geocoder();
        private string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "points.txt");

        public string FileProperty
        {
            //by skipping getter the property would be write-only (rarely used)
            get
            {
                return file;
            }

            //by skipping setter the property would be read-only (occasionally used)
            set
            {
                if (value != null)
                {
                    file = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The file path cannot be null");
                }
            }
        }
        /*public struct Location
        {
            public Location(double posX, double posY, int point, string name, string own = "No owner")
            {
                positionX = posX;
                positionY = posY;
                NAME = name;
                points = point;
                owner = own;
            }

            public double positionX { get; set; }
            public double positionY { get; set; }
            public string NAME { get; set; }
            public int points { get; set; }
            public string owner { get; set; }
        }*/

        public List<MyLocation> LocationList;
        public MapPage()
        {
            InitializeComponent();
            DisplayCurrentLocation();

            LocationList = new List<MyLocation>();
            DisplayExistingPins();
        }

        public async void DisplayExistingPins()
        {
            LocationList.Clear();
            var locations = await LocationDatabase.GetLocations();

            LocationList.AddRange(locations);

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
            var name = await App.Current.MainPage.DisplayPromptAsync("Name of your location", "Name goes here");
            var points = await App.Current.MainPage.DisplayPromptAsync("Value of your location", "Points goes here");
            var xCoord = await App.Current.MainPage.DisplayPromptAsync("X coordinate of your location", "coordinate goes here goes here");
            var yCoord = await App.Current.MainPage.DisplayPromptAsync("Y coordinate of your location", "coordinate goes here goes here");
            await LocationDatabase.AddLocation(double.Parse(xCoord), double.Parse(yCoord), Int32.Parse(points), name);

            DisplayExistingPins();
            //LocationList.Add(new Location(position, Int32.Parse(points), name));
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