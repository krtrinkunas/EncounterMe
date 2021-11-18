using EncounterMeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinInfoPage : ContentPage
    {

        private int locationPoints;
        private string locationOwner;
        private string locationName;
        IPlayerService playerService;
        public PinInfoPage(string name, string owner, int points)
        {
            playerService = DependencyService.Get<IPlayerService>();
            InitializeComponent();
            nameOfPin.Text = name;
            ownerOfPin.Text = "Owner: " + owner;
            pointsOfPin.Text = points.ToString();

            locationPoints = points;
            locationName = name;
            locationOwner = owner;
        }

        private void Occupy_Button_Clicked(object sender, EventArgs e)
        {
            App.player.Points += locationPoints;
            playerService.UpdatePlayer(App.player);
            ownerOfPin.Text = "Owner" + App.player.NickName;
            locationOwner = App.player.NickName;
        }
    }
}