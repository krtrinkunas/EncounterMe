using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;
using EncounterMeApp.Models;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : PopupPage
    {
        ILocationRatingService locationrateService;
        ILocationService locationService;
        MyLocation location;
        LocationRating locationRate;
        int rate;

        public RatingPage(MyLocation location)
        {
            InitializeComponent();
            locationrateService = DependencyService.Get<ILocationRatingService>();
            locationService = DependencyService.Get<ILocationService>();
            this.location = location;
            rate = 0;

            CheckRating();
        }

        private async void CheckRating()
        {
            var ratings = await locationrateService.GetLocationRatings();
            //if null, then there is none
            if (ratings == null || !ratings.Any())
            {
                locationRate = null;
                rate = 0;
            }
            else
            {
                var rating = ratings.SingleOrDefault(c => c.UserId == App.player.Id && c.LocationId == location.Id);
                if (rating != null)
                {
                    locationRate = rating;
                    //rate = Int16.Parse(rating.Rating);lmao
                    //UpdateRating(rating.Rating);
                }
            }

            
        }

        private async void ChangeRating(object sender, EventArgs e)
        {
            UpdateRate((sender as Label).ClassId);
        }

        private void UpdateRate(String rating)
        {
            switch (rating)
            {
                case "1":
                    one.Text = "★";
                    two.Text = "☆";
                    three.Text = "☆";
                    four.Text = "☆";
                    five.Text = "☆";
                    rate = 1;
                    break;
                case "2":
                    one.Text = "★";
                    two.Text = "★";
                    three.Text = "☆";
                    four.Text = "☆";
                    five.Text = "☆";
                    rate = 2;
                    break;
                case "3":
                    one.Text = "★";
                    two.Text = "★";
                    three.Text = "★";
                    four.Text = "☆";
                    five.Text = "☆";
                    rate = 3;
                    break;
                case "4":
                    one.Text = "★";
                    two.Text = "★";
                    three.Text = "★";
                    four.Text = "★";
                    five.Text = "☆";
                    rate = 4;
                    break;
                case "5":
                    one.Text = "★";
                    two.Text = "★";
                    three.Text = "★";
                    four.Text = "★";
                    five.Text = "★";
                    rate = 5;
                    break;
            }
        }

        private async void PostRating(object sender, EventArgs e)
        {
            if (locationRate == null)
            {
                locationRate = CreateRating();
                await locationrateService.AddLocationRating(locationRate);
            }
            else
            {
                //locationRate.Rating = rate;
                await locationrateService.UpdateLocationRating(locationRate);
            }

            location.numberOfRatings++;
            location.rating+=rate;
            await locationService.UpdateLocation(location);
        }

        private LocationRating CreateRating()
        {
            LocationRating loc = new LocationRating();
            loc.LocationId = location.Id;
            loc.UserId = App.player.Id;
            //loc.Rating = rate;
            return loc;
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}