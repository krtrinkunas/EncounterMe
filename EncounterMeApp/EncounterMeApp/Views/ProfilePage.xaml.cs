using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public Player newPlayer;
     
        public ProfilePage()
        {
            InitializeComponent();

            var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";
            newPlayer = new Player { NickName = "Test nickname", LocationsOwned = 2, LocationsVisited = 11, Points = 4856, ProfilePic = image };
        }

        /*
        private string _testNick = "Test String";
        public string TestNick
        {
            get => _testNick;
            set
            {
                if (value != _testNick)
                {
                    _testNick = value;
                    OnPropertyChanged(nameof(TestNick));
                }
            }
        }
        */
    }
}