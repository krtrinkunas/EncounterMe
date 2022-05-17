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
    public partial class FriendPage : ContentPage
    {
        IPlayerService playerService;
        IFriendRequestService friendRequestService;
        IFriendService friendService;

        bool showRequests;
        public FriendPage()
        {
            InitializeComponent();

            showRequests = false;
            playerService = DependencyService.Get<IPlayerService>();
            friendService = DependencyService.Get<IFriendService>();
            friendRequestService = DependencyService.Get<IFriendRequestService>();

            UpdateRequestNumber();
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
        private async void ClickedRequest(object sender, EventArgs e)
        {
            if (showRequests == false)
            {
                showRequests = true;
                UpdateRequestNumber("Hide Requests");

                friendList.IsVisible = false;
                requestList.IsVisible = true;
            }
            else
            {
                showRequests = false;
                UpdateRequestNumber();

                requestList.IsVisible = false;
                friendList.IsVisible = true;
            }
        }
        private async void ViewProfile(object sender, EventArgs e)
        {
            //add profile viewing
            
            FriendRequest newreqst = new FriendRequest();
            newreqst.ReceiverID = App.player.Id;
            newreqst.SenderID = 555;
            await friendRequestService.AddFriendRequest(newreqst);
            
        }

        private async void UpdateRequestNumber(String text = "Show Requests")
        {
            var friendRequestList = await friendRequestService.GetFriendRequests();
            int requestNum = 0;
            foreach (var rqst in friendRequestList)
            {
                //add status?
                if (rqst.ReceiverID == App.player.Id)
                    requestNum++;
            }
            requestButton.Text = text + " (" + requestNum.ToString() + ")";
        }

        private async void AddFriend(object sender, EventArgs e)
        {
            var friendRequestList = await friendRequestService.GetFriendRequests();
            foreach (var rqst in friendRequestList)
            {
                //add status?
                if (rqst.ReceiverID == App.player.Id && rqst.SenderID == int.Parse((sender as Button).ClassId))
                {
                    await friendRequestService.DeleteFriendRequest(rqst);
                    Friend newfriend = new Friend();
                    newfriend.Friend1ID = App.player.Id;
                    newfriend.Friend2ID = rqst.SenderID;
                    await friendService.AddFriend(newfriend);
                    UpdateRequestNumber("Hide Requests");
                    //requestList.RefreshCommand;
                    return;
                }
            }
        }

        private async void DeleteRequest(object sender, EventArgs e)
        {
            var friendRequestList = await friendRequestService.GetFriendRequests();
            foreach (var rqst in friendRequestList)
            {
                //add status?
                if (rqst.ReceiverID == App.player.Id && rqst.SenderID == int.Parse((sender as Button).ClassId))
                {
                    await friendRequestService.DeleteFriendRequest(rqst);
                    UpdateRequestNumber("Hide Requests");
                    //requestList.RefreshCommand;
                    return;
                }
            }
        }

    }
}