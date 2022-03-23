﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Models;
using EncounterMeApp.Services;


namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentSection : ContentPage
    {
        bool showspoilers;

        MyLocation location;
        Player player;
        ICommentService commentService;
        public CommentSection()//MyLocation location, Player player, ICommentService commentService)
        {
            InitializeComponent();
            showspoilers = false;
            //this.location = location;
            //this.player = player;
            //commentService = DependencyService.Get<ICommentService>();
            //this.commentService = commentService;

            
            //missing filter
        }

        public void CreateLayoutForMultipleComments()
        {
            //change
            //StackLayout newStack = new Stack();
            stackLayout.Children.Add(CreateGridForComment("HamsterWasHere", "Prasau veik Prasau veik Prasau veik", new DateTime(2022,3,21), 10, true, true, "discover_button.png"));
            stackLayout.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 100, HeightRequest = 1 });
            stackLayout.Children.Add(CreateGridForComment("HamsterWasNotHere", "Manyciau kad veikia", new DateTime(2022, 3, 20), 12, false, true, "discover_button.png"));
            stackLayout.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 100, HeightRequest = 1 });
            stackLayout.Children.Add(CreateGridForComment("FoxWasHere", "Probs veikia Probs veikiaProbs veikiaProbs veikiaProbs veikiaProbs veikiaProbs veikia", new DateTime(2022, 3, 19), 0, true, false, "discover_button.png"));
        }

        private async void CreateComment(object sender, EventArgs e)
        {
            //doesn't work now
            if (entryComment.Text != "")//prob works
            {
                /*
                Comment comment = new Comment();
                comment.LocationId = location.Id;
                comment.CommentId = new Random().Next(100); // ???
                comment.UserId = player.Id;
                comment.CommentText = entryComment.Text;
                comment.Rating = 0;
                comment.HasSpoilers = spoilerCheckBox.IsChecked;
                comment.HasCaptured = false; //IMPLEMENT WITH NEW CLASS
                comment.TimePosted = DateTime.Now;
                */
                //testLabel.Text = "id: " + location.Id.ToString() + ",idu: "+ player.Id.ToString()+ ",txt: " + entryComment.Text;
                //await commentService.AddComment(comment); //comment service tuscias
                //fix
                /*
                var comments = await commentService.GetComments();
                int result = 0;
                foreach (var com in comments)
                {
                    result++;
                }

                testLabel.Text = "Comment num: "+ result.ToString();
                */
            }

        }

        private async void FilterByRatings(object sender, EventArgs e)
        {
            //implement
        }

        private async void FilterByDate(object sender, EventArgs e)
        {
            //implement
        }

        private async void ShowSpoilers(object sender, EventArgs e)
        {
            //implement
        }

        public Grid CreateGridForComment(String name, String text, DateTime date, int points, bool spoiler, bool captured, String image)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = 70 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateImage(image), 0, 0);
            newGrid.Children.Add(CreateGridForCommentInfo(name, text, date, points, spoiler, captured), 1, 0); //column, row
            return newGrid;
        }

        private Grid CreateGridForCommentInfo(String name, String text, DateTime date, int points, bool spoiler, bool captured)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() {Height = 20 });
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 40 });
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());
            //newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateGridFirstRow(name,spoiler, captured), 0, 0);
            newGrid.Children.Add(CreateLabelText(text), 0, 1);
            newGrid.Children.Add(CreateGridForReview(points, date), 0, 2);

            if(showspoilers == false && spoiler == true)
                newGrid.Children.Add(CreateLabelSpoilerHidden(text), 0, 1);

            //if() prisijunges player atitinka komentaro kureja
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 20 });
            newGrid.Children.Add(CreateGridForEditing(), 0, 3);

            return newGrid;
        }

        private Grid CreateGridFirstRow(String name, bool spoiler, bool captured)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = 150 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 70 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 80});

            newGrid.Children.Add(CreateLabelName(name), 0, 0);
            if (spoiler == true)
                newGrid.Children.Add(SpoilerButton(), 1, 0);
            if (captured == true)
                newGrid.Children.Add(CapturedButton(), 2, 0);

            return newGrid;
        }

        private Grid CreateGridForReview(int points, DateTime date)
        {
            Grid newGrid = new Grid();
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 120 });

            newGrid.Children.Add(CreateLabelText(points.ToString()), 0, 0);
            newGrid.Children.Add(CreateButtonUp(), 1, 0);
            newGrid.Children.Add(CreateButtonDown(), 2, 0);
            newGrid.Children.Add(CreateDateLabel(date), 3, 0);


            return newGrid;
        }

        private Grid CreateGridForEditing()
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 180 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 70 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 70 });

            newGrid.Children.Add(DeleteButton(), 1, 0);
            newGrid.Children.Add(EditButton(), 2, 0);

            return newGrid;
        }

        private Button DeleteButton()
        {
            return new Button
            {
                Text = "DELETE",
                FontSize = 10,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#6CD4FF"),
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 70,
                HeightRequest = 30,
                CornerRadius = 30
            };
        }

        private Button EditButton()
        {
            return new Button
            {
                Text = "EDIT",
                FontSize = 10,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#6CD4FF"),
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 70,
                HeightRequest = 30,
                CornerRadius = 30
            };
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

        private Button CapturedButton()
        {
            return new Button
            {
                Text = "CAPTURED",
                FontSize = 10,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#29E35C"),
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 80,
                HeightRequest = 30,
                CornerRadius = 30
            };
        }
        private Label CreateLabelSpoilerHidden(String text)
        {
            String spoiler = "This comment contains spoilers ";
            if (text.Length <= spoiler.Length)
            {
                return new Label { Text = spoiler, TextColor = Color.White, BackgroundColor = Color.FromHex("#FF4949"), WidthRequest = 270, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, LineBreakMode = LineBreakMode.CharacterWrap };
            }
            else
            {
                //spoiler = spoiler + new string(' ', text.Length - spoiler.Length);
                int height = (text.Length % spoiler.Length + 1) * 4;
                return new Label { Text = spoiler, TextColor = Color.White, BackgroundColor = Color.FromHex("#FF4949"), HeightRequest = height,WidthRequest=270 ,HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, LineBreakMode = LineBreakMode.CharacterWrap };
            }
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

        private Label CreateDateLabel(DateTime date)
        {
            return new Label { Text = date.ToShortDateString(), TextColor = Color.Black, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
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