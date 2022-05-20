using EncounterMeApp.Models;
using EncounterMeApp.Services;
using EncounterMeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaderboardPage : ContentPage
    {
        IPlayerService playerService;
        public LeaderboardPage()
        {
            InitializeComponent();

            playerService = DependencyService.Get<IPlayerService>();


        }

        /*
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        */

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            /*
            var player = ((ListView)sender).SelectedItem as Player;
            if(player == null)
            {
                return;
            }

            await DisplayAlert("Player selected", player.NickName, "OK");*/
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            /*
            ((ListView)sender).SelectedItem = null;
            */
        }

        private async void ClickedFriend(object sender, EventArgs e)
        {
            friendButton.IsEnabled = false;
            globalButton.IsEnabled = true;

            globalList.IsVisible = false;
            friendList.IsVisible = true;
        }
        private async void ClickedGlobal(object sender, EventArgs e)
        {
            globalButton.IsEnabled = false;
            friendButton.IsEnabled = true;

            friendList.IsVisible = false;
            globalList.IsVisible = true;
        }
    }
}