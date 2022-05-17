using EncounterMeApp.Models;
using EncounterMeApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EncounterMeApp.ViewModels
{
    public class FriendViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Player> PlayerFriends { get; set;}
        public ObservableRangeCollection<Player> PlayerRequest { get; set;}

        Player currPlayer;

        IPlayerService playerService;
        IFriendService friendService;
        IFriendRequestService friendRequestService;

        public AsyncCommand RefreshCommand { get; }
        public FriendViewModel()
        {
            Title = "Friends";
            this.currPlayer = App.player;

            PlayerFriends = new ObservableRangeCollection<Player>();
            PlayerRequest = new ObservableRangeCollection<Player>();

            _ = Refresh();
            //var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";

            RefreshCommand = new AsyncCommand(Refresh);

            playerService = DependencyService.Get<IPlayerService>();
            friendService = DependencyService.Get<IFriendService>();
            friendRequestService = DependencyService.Get<IFriendRequestService>();



        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            PlayerFriends.Clear();
            PlayerRequest.Clear();

            var players = await playerService.GetPlayers();
            var friendList = await friendService.GetFriends();
            var friendRequestList = await friendRequestService.GetFriendRequests();

            foreach (var plr in friendList)
            {
                if (plr.Friend1ID == currPlayer.Id)
                    PlayerFriends.Add(await playerService.GetPlayer(plr.Friend2ID));
                else if (plr.Friend2ID == currPlayer.Id)
                    PlayerFriends.Add(await playerService.GetPlayer(plr.Friend1ID));
            }
            PlayerFriends.Sort((a, b) => { return a.CompareTo(b); });

            foreach (var rqst in friendRequestList)
            {
                //add status?
                if (rqst.ReceiverID == currPlayer.Id)
                    PlayerRequest.Add(await playerService.GetPlayer(rqst.SenderID));
            }


            IsBusy = false;
        }
    }
}
