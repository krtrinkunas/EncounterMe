﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;
using EncounterMeApp.Models;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        IPlayerService playerService;
        public RegistrationPage()
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
        }

        Random random = new Random();
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //Saving registration data
            if (!string.IsNullOrEmpty(entryUserName.Text) && !string.IsNullOrEmpty(entryEmail.Text) 
                && !string.IsNullOrEmpty(entryFirstName.Text) && !string.IsNullOrEmpty(entryLastName.Text) 
                && !string.IsNullOrEmpty(entryPassword.Text))
            {
                var newId = random.Next(100);
                var newPlayer = new Player { NickName = entryUserName.Text, Points = 0, Email = entryEmail.Text, Id = newId, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = entryFirstName.Text, Lastname = entryLastName.Text, Password = entryPassword.Text };
                await playerService.AddPlayer(newPlayer);
                await DisplayAlert("", "Registration Successful!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Registration Failed!", "You left some required fields empty", "OK");
            }
        }
    }
}