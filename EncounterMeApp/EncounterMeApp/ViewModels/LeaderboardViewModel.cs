using EncounterMeApp.Models;
using EncounterMeApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EncounterMeApp.ViewModels
{
    public class LeaderboardViewModel : BaseViewModel //Imported from MVVM helpers
    {
        public ObservableRangeCollection<Player> Player { get; set; }

        IPlayerService playerService;
        public Player this[int key]
        {
            get => Player[key];
            set => Player[key] = value;
        }


        public AsyncCommand RefreshCommand { get; }
        //public AsyncCommand AddCommand { get; }
        public AsyncCommand<Player> RemoveCommand { get; }
        public LeaderboardViewModel()
        {
            Title = "Leaderboard";

            Player = new ObservableRangeCollection<Player>();

            _ = Refresh();
            //var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";

            RefreshCommand = new AsyncCommand(Refresh);
            //AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Player>(Remove);

            playerService = DependencyService.Get<IPlayerService>();
        }

        async Task Remove(Player player)
        {
            await playerService.DeletePlayer(player);
            await Refresh();
        }
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Player.Clear();

            var players = await playerService.GetPlayers();

            Player.AddRange(players);
            //Player.SortDesc();

            IsBusy = false;
        }
    }
}
