using EncounterMeApp.Models;
using EncounterMeApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EncounterMeApp.ViewModels
{
    public class LeaderboardViewModel : BaseViewModel //Imported from MVVM helpers
    {
        public ObservableRangeCollection<Player> Player { get; set; }

        public Player this[int key]
        {
            get => Player[key];
            set => Player[key] = value;
        }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Player> RemoveCommand { get; }
        public LeaderboardViewModel()
        {
            Title = "Leaderboard";

            Player = new ObservableRangeCollection<Player>();

            //var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";
            /*
            Player.Add(new Player { NickName = "Destroyer3000", Points = 2894, ProfilePic = image });
            Player.Add(new Player { NickName = "Enjoyer69420", Points = 468465, ProfilePic = image });
            Player.Add(new Player { NickName = "Dinosower", Points = 4881, ProfilePic = image });
            Player.Add(new Player { NickName = "GetRekt", Points = 89610, ProfilePic = image });
            Player.Add(new Player { NickName = "1336", Points = 8412, ProfilePic = image });
            Player.Add(new Player { NickName = "Ninja", Points = 21, ProfilePic = image });
            Player.Add(new Player { NickName = "Shroud", Points = 51127, ProfilePic = image });
            Player.Add(new Player { NickName = "PashaBiceps", Points = 819856, ProfilePic = image });
            Player.Add(new Player { NickName = "NoScope", Points = 754, ProfilePic = image });*/

            //Player.SortDesc();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Player>(Remove);
        }

        async Task Add()
        {
            var nickName = await App.Current.MainPage.DisplayPromptAsync("Name", "Name goes here");
            var points = await App.Current.MainPage.DisplayPromptAsync("Points", "Points goes here");
            await PlayerDatabase.AddPlayer(nickName, Int32.Parse(points));
            await Refresh();
        }
        async Task Remove(Player player)
        {
            await PlayerDatabase.RemovePlayer(player.Id);
            await Refresh();
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Player.Clear();

            var players = await PlayerDatabase.GetPlayers();

            Player.AddRange(players);
            //Player.SortDesc();

            IsBusy = false;
        }
    }
}
