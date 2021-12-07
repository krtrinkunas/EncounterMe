using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;
using EncounterMeApp.Models;
using System.Text.RegularExpressions;
using EncounterMeApp.ViewModels;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        string entryUserName = "";
        string entryEmail = "";
        string entryFirstName = "";
        string entryLastName = "";
        string entryPassword = "";

        public string EntryUserName
        {
            get => entryUserName;
            set
            {
                if (value == entryUserName)
                    return;
                entryUserName = value;
                OnPropertyChanged();
            }
        }

        public string EntryEmail
        {
            get => entryEmail;
            set
            {
                if (value == entryEmail)
                    return;
               entryEmail = value;
                OnPropertyChanged();
            }
        }

        public string EntryFirstName
        {
            get => entryFirstName;
            set
            {
                if (value == entryFirstName)
                    return;
                entryFirstName = value;
                OnPropertyChanged();
            }
        }

        public string EntryLastName
        {
            get => entryLastName;
            set
            {
                if (value == entryLastName)
                    return;
                entryLastName = value;
                OnPropertyChanged();
            }
        }

        public string EntryPassword
        {
            get => entryPassword;
            set
            {
                if (value == entryPassword)
                    return;
                entryPassword = value;
                OnPropertyChanged();
            }
        }

        Random random = new Random();
        PlayerValidation playerValidation = new PlayerValidation();
        private async void btnRegister_Clicked(object sender, EventArgs e) //
        {
            
            var newId = random.Next(100);
            var newPlayer = new Player { NickName = entryUserName, Points = 0, Email = entryEmail, Id = newId, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = entryFirstName, Lastname = entryLastName, Password = entryPassword };
            Player validatedPlayer = await playerValidation.validateUser(newPlayer);

            if (validatedPlayer == null)
            {
                await DisplayAlert("Registration Failed!", "You left some required fields empty.", "OK");
            }
            else
            {
                await DisplayAlert("", "Registration Successful!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}