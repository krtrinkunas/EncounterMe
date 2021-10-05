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
        */

        public string DisplayName => "TEST PEPA";
        /*
        void OnPropertyChanged(string name)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
        */
    }
}
