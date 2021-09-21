using EncounterMeApp.Models;
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

        public AsyncCommand RefreshCommand { get; }
        public LeaderboardViewModel()
        {
            Title = "Leaderboard";

            Player = new ObservableRangeCollection<Player>();

            var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";

            Player.Add(new Player { NickName = "Destroyer3000", Points = "2000 points", ProfilePic = image });
            Player.Add(new Player { NickName = "Enjoyer3000", Points = "1000 points", ProfilePic = image });
            Player.Add(new Player { NickName = "Dinosower3000", Points = "500 points", ProfilePic = image });

            RefreshCommand = new AsyncCommand(Refresh);
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }
    }
}
