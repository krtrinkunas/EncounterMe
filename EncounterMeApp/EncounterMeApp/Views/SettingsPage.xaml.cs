﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            ThemeSwitch.IsToggled = Preferences.Get("ThemeSwitch", false);
        }

        private void Save_Button_Clicked(object sender, EventArgs e) //To define, what needs to be saved
        {
            Preferences.Set("ThemeSwitch", ThemeSwitch.IsToggled);
        }

        private async void LogOut_Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Are you sure you want to Log out?", "Think twice", "OK");
        }
        private void Reset_Button_Clicked(object sender, EventArgs e)//To clear, what has been saved
        {
            Preferences.Clear();
        }
    }
}