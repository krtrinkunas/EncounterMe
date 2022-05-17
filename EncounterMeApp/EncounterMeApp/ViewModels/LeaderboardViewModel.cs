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
    public class LeaderboardViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Player> Player1 { get; set; }
        public ObservableRangeCollection<Player> PlayerFriends { get; set;}

        Player currPlayer;

        IPlayerService playerService;
        IFriendService friendService;

        public AsyncCommand RefreshCommand { get; }
        public LeaderboardViewModel()
        {
            Title = "Leaderboard";
            this.currPlayer = App.player;

            Player1 = new ObservableRangeCollection<Player>();
            PlayerFriends = new ObservableRangeCollection<Player>();

            _ = Refresh();
            //var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";

            RefreshCommand = new AsyncCommand(Refresh);

            playerService = DependencyService.Get<IPlayerService>();
            friendService = DependencyService.Get<IFriendService>();

            
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Player1.Clear();
            PlayerFriends.Clear();

            var players = await playerService.GetPlayers();
            var friendList = await friendService.GetFriends();

            foreach (var plr in friendList)
            {
                if (plr.Friend1ID == currPlayer.Id)
                    PlayerFriends.Add(await playerService.GetPlayer(plr.Friend2ID));
                else if (plr.Friend2ID == currPlayer.Id)
                    PlayerFriends.Add(await playerService.GetPlayer(plr.Friend1ID));
            }

            Player1.AddRange(players);
            Player1.Sort((a, b) => { return a.CompareTo(b); });

            //PlayerFriends.AddRange(players);
            PlayerFriends.Sort((a, b) => { return a.CompareTo(b); });
            //PlayerFriends.Sort((a) => {return a}) //((a, b) => { return a.CompareTo(b); });

            IsBusy = false;
        }
    }
}
