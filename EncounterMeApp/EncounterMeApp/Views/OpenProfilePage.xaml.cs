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
    public partial class OpenProfilePage : PopupPage
    {
        IFriendRequestService friendRequestService;
        IFriendService friendService;
        IBlockService blockService;

        Player player;
        Block isUserBlocked;
        Block isUserBlocking;
        Friend isFriend;
        FriendRequest isPending;
        FriendRequest isRequest;

        public OpenProfilePage(Player player)
        {
            InitializeComponent();

            friendService = DependencyService.Get<IFriendService>();
            friendRequestService = DependencyService.Get<IFriendRequestService>();
            blockService = DependencyService.Get<IBlockService>();

            
            this.player = player;

            FillInformation();

            IsUserBlocked();
            IsUserBlocking();

            IsFriend();
            IsPending();
            IsRequest();
            SetButtons();

            //check if the same user
            if (App.player.Id == player.Id)
                HideButtons();
        }

        public void GetInformation()
        {
            IsUserBlocked();
            IsUserBlocking();

            IsFriend();
            IsPending();
            IsRequest();
            //SetButtons();
        }

        private void FillInformation()
        {
            ProfileLabel.Text = player.NickName;
            ProfileImage.Source = player.ProfilePic;
            currentEmail.Text = player.Email;
            pointsLabel.Text = player.Points.ToString();
            visitedPlacesLabel.Text = player.LocationsVisited.ToString();
            ownedPlacesLabel.Text = player.LocationsOwned.ToString();
        }

        private void SetButtons()
        {
            HideButtons();

            if (isUserBlocked != null)
            {
                AddButton.IsVisible = true;
                AddButton.IsEnabled = false;
                AddButton.BackgroundColor = Color.FromHex("#6b6b6b");
            }
            else if (isUserBlocking != null)
            {
                AddButton.IsVisible = true;
                AddButton.IsEnabled = false;
                AddButton.BackgroundColor = Color.FromHex("#6b6b6b");

                UnblockButton.IsVisible = true;
            }
            else if (isFriend != null)
            {
                RemoveButton.IsVisible = true;
            }
            else if (isPending != null)
            {
                CancelButton.IsVisible = true;
                BlockButton.IsVisible = true;
            }
            else if (isRequest != null)
            {
                AcceptRequestButton.IsVisible = true;
                DeclineRequestButton.IsVisible = true;
                BlockButton.IsVisible = true;
            }
            else
            {
                AddButton.IsVisible = true;
                BlockButton.IsVisible = true;
            }
        }

        private void HideButtons()
        {
            AddButton.IsVisible = false;
            AddButton.IsEnabled = true;
            AddButton.BackgroundColor = Color.FromHex("#72FF6C");
            UnblockButton.IsVisible = false;
            RemoveButton.IsVisible = false;
            CancelButton.IsVisible = false;
            BlockButton.IsVisible = false;
            AcceptRequestButton.IsVisible = false;
            DeclineRequestButton.IsVisible = false;
        }

        private async void IsUserBlocked()
        {
            var blocks = await blockService.GetBlocks();

            
            foreach (var block in blocks)
            {
                if (App.player.Id == block.UserBlockedID && player.Id == block.BlockedByID)
                {
                    isUserBlocked = block;
                    AddButton.IsVisible = true;
                    AddButton.IsEnabled = false;
                    return;
                }
            }
            SetButtons();
            isUserBlocked = null;
        }

        private async void IsUserBlocking()
        {
            var blocks = await blockService.GetBlocks();

            /*
            if (blocks == null || !blocks.Any())
            {
                isUserBlocking = null;
                AddButton.IsVisible = true;
                AddButton.IsEnabled = false;

                UnblockButton.IsVisible = true;
                return;
            }
            */

            foreach (var block in blocks)
            {
                if (App.player.Id == block.BlockedByID && player.Id == block.UserBlockedID)
                {
                    isUserBlocking = block;
                    SetButtons();
                    return;
                }
            }
            SetButtons();
            isUserBlocking = null;
        }

        private async void IsFriend()
        {
            var friends = await friendService.GetFriends();

            /*
            if (friends == null)
            {
                isFriend = null;
                return;
            }
            */
            foreach (var frnd in friends)
            {
                if ((App.player.Id == frnd.Friend1ID && player.Id == frnd.Friend2ID) ||
                    (App.player.Id == frnd.Friend2ID && player.Id == frnd.Friend1ID))
                {
                    isFriend = frnd;
                    SetButtons();
                    return;
                }
            }
            SetButtons();
            isFriend = null;
        }

        private async void IsPending()
        {
            var requests = await friendRequestService.GetFriendRequests();

            /*
            if (requests == null)
            {
                isPending = null;
                return;
            }*/

            foreach (var rqst in requests)
            {
                if (App.player.Id == rqst.SenderID && player.Id == rqst.ReceiverID)
                {
                    isPending = rqst;
                    SetButtons();
                    return;
                }
            }
            SetButtons();
            isPending = null;
        }

        private async void IsRequest()
        {
            var requests = await friendRequestService.GetFriendRequests();

            /*
            if (requests == null)
            {
                isRequest = null;
                return;
            }*/

            foreach (var rqst in requests)
            {
                if (App.player.Id == rqst.ReceiverID && player.Id == rqst.SenderID)
                {
                    isRequest = rqst;
                    SetButtons();
                    return;
                }
            }
            SetButtons();
            isRequest = null;
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
        private async void SendRequest(object sender, EventArgs e)
        {
            FriendRequest rqst = new FriendRequest();
            rqst.Id = (new Random()).Next(1,99999);
            rqst.SenderID = App.player.Id;
            rqst.ReceiverID = player.Id;
            isPending = rqst;
            await friendRequestService.AddFriendRequest(rqst);

            SetButtons();
        }
        private async void CancelRequest(object sender, EventArgs e)
        {
            await friendRequestService.DeleteFriendRequest(isPending);
            isPending = null;

            SetButtons();
        }
        private async void AcceptRequest(object sender, EventArgs e)
        {
            Friend frnd = new Friend();
            frnd.Id = (new Random()).Next(1, 99999);
            frnd.Friend1ID = App.player.Id;
            frnd.Friend2ID = player.Id;
            await friendService.AddFriend(frnd);
            isFriend = frnd;

            await friendRequestService.DeleteFriendRequest(isRequest);
            isRequest = null;

            SetButtons();
        }
        private async void DeclineRequest(object sender, EventArgs e)
        {
            await friendRequestService.DeleteFriendRequest(isRequest);
            isRequest = null;

            SetButtons();
        }
        private async void BlockPlayer(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Block User", "Are you sure you want to block this user?", "Yes", "Cancel");
            if (action)
            {
                Block blck = new Block();
                blck.Id = (new Random()).Next(1, 99999);
                blck.BlockedByID = App.player.Id;
                blck.UserBlockedID = player.Id;
                await blockService.AddBlock(blck);
                isUserBlocking = blck;
                if (isRequest != null)
                    await friendRequestService.DeleteFriendRequest(isRequest);
                if (isPending != null)
                    await friendRequestService.DeleteFriendRequest(isPending);
                isRequest = null;
                isPending = null;
            }

            SetButtons();
        }
        private async void RemoveFriend(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Remove Friend", "Are you sure you want to remove friend?", "Yes", "Cancel");
            if (action)
            {
                await friendService.DeleteFriend(isFriend);
                isFriend = null;
            }

            SetButtons();
        }

        private async void UnblockPlayer(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Unblock User", "Are you sure you want to unblock this user?", "Yes", "Cancel");
            if (action)
            {
                await blockService.DeleteBlock(isUserBlocking);
                isUserBlocking = null;
            }

            SetButtons();
        }        /*
         bool action = await DisplayAlert("Deletion", "Are you sure you want to remove this comment?", "Yes", "Cancel");
            if (action)
            {
                var comment = await commentService.GetComment(Int16.Parse((sender as Button).ClassId));
                await commentService.DeleteComment(comment);
                CreateLayoutForMultipleComments();
            } 
      */
    }
}