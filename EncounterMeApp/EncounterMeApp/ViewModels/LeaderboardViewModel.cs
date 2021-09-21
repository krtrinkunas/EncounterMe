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

            Player.Add(new Player { NickName = "Destroyer3000", Points = "2894 points", ProfilePic = image });
            Player.Add(new Player { NickName = "Enjoyer69420", Points = "468465 points", ProfilePic = image });
            Player.Add(new Player { NickName = "Dinosower", Points = "4881 points", ProfilePic = image });
            Player.Add(new Player { NickName = "GetRekt", Points = "89610 points", ProfilePic = image });
            Player.Add(new Player { NickName = "1336", Points = "8412 points", ProfilePic = image });
            Player.Add(new Player { NickName = "Ninja", Points = "21 points", ProfilePic = image });
            Player.Add(new Player { NickName = "Shroud", Points = "51127 points", ProfilePic = image });
            Player.Add(new Player { NickName = "PashaBiceps", Points = "819856 points", ProfilePic = image });
            Player.Add(new Player { NickName = "NoScope", Points = "754 points", ProfilePic = image });

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
