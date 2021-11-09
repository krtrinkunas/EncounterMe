using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Services;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            //Saving registration data
            await DisplayAlert("","Registration Successful!", "OK");
            await PlayerDatabase.AddPlayer(entryUserName.Text, 0, entryFirstName.Text, entryLastName.Text, entryPassword.Text, entryEmail.Text);
            await Navigation.PopAsync();
        }
    }
}