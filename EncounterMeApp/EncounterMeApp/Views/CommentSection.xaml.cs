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
    public partial class CommentSection : ContentPage
    {
        public CommentSection()
        {
            
            InitializeComponent();
            //missing filter
        }

        public void CreateLayoutForMultipleComments()
        {
            //change
            //StackLayout newStack = new Stack();
            stackLayout.Children.Add(CreateGridForComment("HamsterWasHere", "Prasau veik Prasau veik Prasau veik", 10, true, "discover_button.png"));
            stackLayout.Children.Add(CreateGridForComment("HamsterWasNotHere", "Manyciau kad veikia", 12, false, "discover_button.png"));
            stackLayout.Children.Add(CreateGridForComment("FoxWasHere", "Probs veikia Probs veikiaProbs veikiaProbs veikiaProbs veikiaProbs veikiaProbs veikia", 0, true, "discover_button.png"));
        }
        public Grid CreateGridForComment(String name, String text, int points, bool spoiler, String image)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = 70 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateImage(image), 0, 0);
            newGrid.Children.Add(CreateGridForCommentInfo(name, text, points, spoiler), 1, 0); //column, row
            return newGrid;
        }

        private Grid CreateGridForCommentInfo(String name, String text, int points, bool spoiler)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() {Height = 20 });
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 40 });
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateLabelName(name), 0, 0);
            newGrid.Children.Add(CreateLabelText(text), 0, 1);
            newGrid.Children.Add(CreateGridForReview(points, spoiler), 0, 2);

            return newGrid;
        }

        private Grid CreateGridForReview(int points, bool spoiler)
        {
            Grid newGrid = new Grid();
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 120 });

            newGrid.Children.Add(CreateLabelText(points.ToString()), 0, 0);
            newGrid.Children.Add(CreateButtonUp(), 1, 0);
            newGrid.Children.Add(CreateButtonDown(), 2, 0);
            if(spoiler == true)
                newGrid.Children.Add(SpoilerButton(), 3, 0);

            return newGrid;
        }

        private Button SpoilerButton()
        {
            return new Button
            {
                Text = "SPOILER",
                FontSize = 10,
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 70,
                HeightRequest = 30,
                CornerRadius = 30
            };
        }

        private Label CreateLabelText(String text)
        {
            return new Label { Text = text, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center };
        }
        private Label CreateLabelName(String text)
        {
            return new Label { Text = text, FontAttributes = FontAttributes.Bold, TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center };
        }

        private Image CreateButtonUp()
        {
            Image image = new Image
            {
                Source = "green.png"
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                GoBack(s,e); //change
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);

            return image;
        }

        private Image CreateButtonDown()
        {
            Image image = new Image
            {
                Source = "red.png"
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                GoBack(s, e); //change
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);

            return image;
        }

        private Image CreateImage(String link)
        {
            return new Image { Source = link, Margin = 5, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Start };
        }
        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}