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
    public partial class PinInfoPage : ContentPage
    {
        public PinInfoPage(string name, string owner, int points)
        {
            InitializeComponent();
            nameOfPin.Text = name;
            ownerOfPin.Text = "Owner: " + owner;
            pointsOfPin.Text = points.ToString();
        }
    }
}