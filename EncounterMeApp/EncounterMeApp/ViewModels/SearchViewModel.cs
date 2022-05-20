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
    public class SearchViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Player> Player { get; set;}

        IPlayerService playerService;

        public AsyncCommand RefreshCommand { get; }
        public SearchViewModel()
        {
            Title = "Search";

            Player = new ObservableRangeCollection<Player>();

            _ = Refresh();
            //var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";

            RefreshCommand = new AsyncCommand(Refresh);

            playerService = DependencyService.Get<IPlayerService>();



        }

        public async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Player.Clear();

            IsBusy = false;
        }

        public async Task RefreshPlayers(String name)
        {
            IsBusy = true;

            await Task.Delay(2000);

            Player.Clear();
            
            var players = await playerService.GetPlayers();

            foreach (var plr in players)
            {
                if (plr.NickName.Contains(name))
                    Player.Add(await playerService.GetPlayer(plr.Id));
            }
            
            IsBusy = false;
        }

    }
}
