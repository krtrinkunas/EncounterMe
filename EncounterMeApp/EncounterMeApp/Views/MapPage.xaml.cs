using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            DisplayCurrentLocation();
            DisplayExistingPins();
        }

        public void DisplayExistingPins()
        {
            var pin1 = new Pin()
            {
                Position = new Position (54.684084, 25.276133),
                Label = "Petro Cvirkos aikštė"
            };
            var pin2 = new Pin()
            {
                Position = new Position(54.685372, 25.286621),
                Label = "Katedra"
            };
            mapOfVilnius.Pins.Add(pin1);
            pin1.MarkerClicked += Pin1_MarkerClicked;
            mapOfVilnius.Pins.Add(pin2);
        }

        private void Pin1_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            DisplayActionSheet("Petro Cvirkos aikštė", "Cancel", null, "Owner","Occupy");
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

        private void mapOfVilnius_MapClicked(object sender, MapClickedEventArgs e)
        {

        }
    }
}