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
    public partial class LeaderboardPage : ContentPage
    {
        public LeaderboardPage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var player = ((ListView)sender).SelectedItem as Player;
            if(player == null)
            {
                return;
            }

            await DisplayAlert("Player selected", player.NickName, "OK");
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var player = ((MenuItem)sender).BindingContext as Player;

            if (player == null)
            {
                return;
            }

            await DisplayAlert("Player added as a friend", player.NickName, "OK");
        }
    }
}