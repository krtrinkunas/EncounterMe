using Xamarin.Forms;
using EncounterMeApp.Views;
using EncounterMeApp.Models;

namespace EncounterMeApp
{
    public partial class App : Application
    {
        public static Player player { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
