using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EncounterMeApp.ViewModels
{
    public class ProfilePageViewModel //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        /*
        private string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                {
                    return;
                }
                
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
 
       void OnPropertyChanged(string name)
       {
           PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
       }
       */

        //Case for data binding
        /*
        public Player newPlayer;

        public ProfilePageViewModel()
        {
            var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";
            newPlayer = new Player { NickName = "Doomer McFeelsman", LocationsOwned = 2, LocationsVisited = 11, Points = 4856, ProfilePic = image, Type = PlayerType.Creator }; //Kaip padaryti, kad galetu prieit tiesiogiai, kad nereiketu veliau priskirinet?
        }
        //Kodel neveikia su paprastu '='?
        public string TestNick => newPlayer.NickName;
        public int TestOwned => newPlayer.LocationsOwned;
        public int TestVisited => newPlayer.LocationsVisited;
        public int TestPoints => newPlayer.Points;
        public string TestImage => newPlayer.ProfilePic;
        public PlayerType TestType => newPlayer.Type;
        */
    }
}
