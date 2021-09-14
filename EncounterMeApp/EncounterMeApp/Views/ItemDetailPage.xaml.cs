using EncounterMeApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EncounterMeApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}