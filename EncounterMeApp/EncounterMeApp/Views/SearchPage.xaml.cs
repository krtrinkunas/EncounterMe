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
    public partial class SearchPage : ContentPage
    {
        IPlayerService playerService;
        IFriendRequestService friendRequestService;
        IFriendService friendService;

        public SearchPage()
        {
            InitializeComponent();

            playerService = DependencyService.Get<IPlayerService>();
            friendService = DependencyService.Get<IFriendService>();
            friendRequestService = DependencyService.Get<IFriendRequestService>();
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

        private async void ViewProfile(object sender, EventArgs e)
        {
            //add profile viewing
            
            //adding friend request for testing 
            FriendRequest newreqst = new FriendRequest();
            newreqst.ReceiverID = App.player.Id;
            newreqst.SenderID = 555;
            await friendRequestService.AddFriendRequest(newreqst);
            
        }

        private async void ClickedSearch(object sender, EventArgs e)
        {
            String name = searchEntry.Text;
            var players = await playerService.GetPlayers();
            int count = 0;

            foreach (var plr in players)
            {
                if (plr.NickName.Contains(name))
                    count++;
            }
            if (count == 0)
                searchLabel.IsVisible = true;
            else
                searchLabel.IsVisible = false;

            SearchViewModel Viewmodel = (SearchViewModel)this.BindingContext;
            await Viewmodel.RefreshPlayers(name);
            
            
        }
    }
}