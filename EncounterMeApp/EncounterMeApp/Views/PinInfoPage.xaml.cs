using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using EncounterMeApp.Models;
using EncounterMeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncounterMeApp;
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
        ICaptureAttemptService captureService;
        CaptureAttempt captureAttempt;
        public PinInfoPage(MyLocation location)
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
            locationService = DependencyService.Get<ILocationService>();
            captureService = DependencyService.Get<ICaptureAttemptService>();

            nameOfPin.Text = location.NAME;
            ownerOfPin.Text = "Owner: " + location.owner;
            pointsOfPin.Text = location.points.ToString();

            currentLocation = location;

            GetCaptureAttempt();
        }

        private async void GetCaptureAttempt()
        {
            var captures = await captureService.GetCaptureAttempts();
            //if null, then there is none
            if (captures == null || !captures.Any())
                captureAttempt = null;
            else
                captureAttempt = captures.SingleOrDefault(c => c.UserId == App.player.Id && c.LocationId == currentLocation.Id);

            if (currentLocation.owner == App.player.NickName && captureAttempt == null)
            {
                captureAttempt = CreateCaptureAttempt();
                //because author so it automatically is captured (no author tag for now)
                captureAttempt.HasCaptured = true;
                await captureService.AddCaptureAttempt(captureAttempt);
            }
        }

        private async void CheckCaptureAttempt()
        {
            var captures = await captureService.GetCaptureAttempts();
            CaptureAttempt capture;

            if (captures == null || !captures.Any())
            {
                capture = CreateCaptureAttempt();
                captureAttempt = capture;
                await captureService.AddCaptureAttempt(capture);
                return;
            }
            
            capture = captures.SingleOrDefault(c => c.UserId == App.player.Id && c.LocationId == currentLocation.Id);
            
            if (capture == null)
            {
                capture = CreateCaptureAttempt();
                captureAttempt = capture;
                await captureService.AddCaptureAttempt(capture);
                return;
            }
            
            captureAttempt = capture;
        }

        private CaptureAttempt CreateCaptureAttempt()
        {
            CaptureAttempt newCapture = new CaptureAttempt();
            newCapture.HasCaptured = false;
            newCapture.LocationId = currentLocation.Id;
            newCapture.UserId = App.player.Id;

            return newCapture;
        }

        private async void Occupy_Button_Clicked(object sender, EventArgs e)
        {
            if (App.player.NickName != currentLocation.owner)
            {
                //moved capture attempt
                CheckCaptureAttempt();

                var currentCoords = await Geolocation.GetLastKnownLocationAsync();
                if (Location.CalculateDistance(currentLocation.positionX, currentLocation.positionY, currentCoords.Latitude, currentCoords.Longitude, 0) <= 1)
                {
                    string answer = await DisplayPromptAsync(currentLocation.question, "Answer goes here");
                    if (answer == currentLocation.answer)
                    {
                        App.player.Points += currentLocation.points;
                        App.player.LocationsOwned += 1;

                        //capture update
                        captureAttempt.HasCaptured = true;
                        await captureService.UpdateCaptureAttempt(captureAttempt);

                        var PlayerList = new List<Player>();
                        PlayerList.Clear();
                        var players = await playerService.GetPlayers();
                        PlayerList.AddRange(players);
                        Player newPlayer = PlayerList.Find(delegate (Player play)
                        {
                            return play.NickName == currentLocation.owner;
                        });
                        if (newPlayer != null)
                        {
                            newPlayer.LocationsOwned--;
                            await playerService.UpdatePlayer(newPlayer);
                        }

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

        private async void OpenCommentSection(object sender, EventArgs e)
        {
            //check if capture attempt was made
            CommentSectionPage commentSection = new CommentSectionPage(currentLocation, App.player, captureAttempt);
            await Navigation.PushAsync(commentSection);
        }
    }
}