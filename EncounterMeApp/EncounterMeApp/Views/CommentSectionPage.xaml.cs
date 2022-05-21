using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Models;
using EncounterMeApp.Services;


namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentSectionPage : ContentPage
    {
        bool showspoilers;
        bool filterbydate;
        bool filterbyratings;

        MyLocation location;
        Player player;
        ICommentService commentService;
        IPlayerService playerService;
        ICommentRatingService comratingService;

        CaptureAttempt captureAttempt;
        public CommentSectionPage(MyLocation location, Player player, CaptureAttempt captureAttempt)
        {
            InitializeComponent();
            this.location = location;
            this.player = player;
            this.captureAttempt = captureAttempt;
            commentService = DependencyService.Get<ICommentService>();
            playerService = DependencyService.Get<IPlayerService>();
            comratingService = DependencyService.Get<ICommentRatingService>();

            showspoilers = false;
            filterbydate = false;
            filterbyratings = false;

            userImage.Source = player.ProfilePic;
            CreateLayoutForMultipleComments();
        }

        /*
        public void CreateLayoutForMultipleCommentsLegacy()
        {
            commentService = DependencyService.Get<ICommentService>();
            //change
            //StackLayout newStack = new Stack();
            stackLayout.Children.Add(CreateGridForComment("HamsterWasHere", "Prasau veik Prasau veik Prasau veik", new DateTime(2022,3,21), 10, true, true, "discover_button.png"));
            stackLayout.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 100, HeightRequest = 1 });
            stackLayout.Children.Add(CreateGridForComment("HamsterWasNotHere", "Manyciau kad veikia", new DateTime(2022, 3, 20), 12, false, true, "discover_button.png"));
            stackLayout.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 100, HeightRequest = 1 });
            stackLayout.Children.Add(CreateGridForComment("FoxWasHere", "Probs veikia Probs veikiaProbs veikiaProbs veikiaProbs veikiaProbs veikiaProbs veikia", new DateTime(2022, 3, 19), 0, true, false, "discover_button.png"));
        }
        */

        public async void CreateLayoutForMultipleComments()
        {
            stackLayout.Children.Clear();

            var comments = await commentService.GetComments();

            if (filterbydate && filterbyratings)
                comments = comments.OrderByDescending(x => x.Rating).ThenBy(x => x.TimePosted);
            else if (filterbyratings)
                comments = comments.OrderByDescending(x => x.Rating);
            else if (filterbydate)
                comments = comments.OrderByDescending(x => x.TimePosted);

            foreach (var com in comments)
            {
                if (location.Id == com.LocationId)
                {
                    var locplayer = await playerService.GetPlayer(com.UserId);
                    bool edit = player.Id == com.UserId ? true : false;

                    stackLayout.Children.Add(
                        CreateGridForComment(
                            com.UserId,
                            com.CommentId,
                            locplayer.NickName,
                            com.CommentText,
                            com.TimePosted,
                            com.Rating,
                            com.HasSpoilers, //spoiler 
                            com.HasCaptured, //captured
                            edit,
                            locplayer.ProfilePic));//"discover_button.png"));
                    //add line
                    stackLayout.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 100, HeightRequest = 1 });
                }
            }
        }

        private async void CreateComment(object sender, EventArgs e)
        {
            if (captureAttempt == null)
            {
                await DisplayAlert("Cannot Post", "You cannot comment without trying to occupy the location.", "OK");
            }
            else
            {
                if (entryComment.Text != "")//prob works
                {

                    Comment comment = new Comment();
                    comment.LocationId = location.Id;
                    //comment.CommentId = new Random().Next(100); // ???
                    comment.UserId = player.Id;
                    comment.CommentText = entryComment.Text;
                    comment.Rating = 0;
                    comment.HasSpoilers = spoilerCheckBox.IsChecked;
                    comment.HasCaptured = captureAttempt.HasCaptured;
                    comment.TimePosted = DateTime.Now;

                    await commentService.AddComment(comment);

                    entryComment.Text = "";
                    CreateLayoutForMultipleComments();
                }
            }

        }

        private async void FilterByRatings(object sender, EventArgs e)
        {
            if (filterbyratings)
            {
                filterbyratings = false;
                (sender as Button).BackgroundColor = Color.White;
                (sender as Button).TextColor = Color.FromHex("#6CD4FF");
            }
            else
            {
                filterbyratings = true;
                (sender as Button).BackgroundColor = Color.FromHex("#6CD4FF");
                (sender as Button).TextColor = Color.White;
            }
                
            CreateLayoutForMultipleComments();
        }

        private async void FilterByDate(object sender, EventArgs e)
        {
            if (filterbydate)
            {
                filterbydate = false;
                (sender as Button).BackgroundColor = Color.White;
                (sender as Button).TextColor = Color.FromHex("#29E35C");
            }
            else
            {
                filterbydate = true;
                (sender as Button).BackgroundColor = Color.FromHex("#29E35C");
                (sender as Button).TextColor = Color.White;
            }

            CreateLayoutForMultipleComments();
        }

        private async void ShowSpoilers(object sender, EventArgs e)
        {
            if (showspoilers)
            {
                showspoilers = false;
                (sender as Button).BackgroundColor = Color.White;
                (sender as Button).TextColor = Color.Red;
            }
            else
            {
                showspoilers = true;
                (sender as Button).BackgroundColor = Color.Red;
                (sender as Button).TextColor = Color.White;
            }
                
            CreateLayoutForMultipleComments();
        }

        public Grid CreateGridForComment(int userId, int id, String name, String text, DateTime date, int points, bool spoiler, bool captured, bool edit, String image)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = 70 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateImage(image, userId), 0, 0);
            newGrid.Children.Add(CreateGridForCommentInfo(id, name, text, date, points, spoiler, captured, edit), 1, 0); //column, row
            return newGrid;
        }

        private Grid CreateGridForCommentInfo(int id, String name, String text, DateTime date, int points, bool spoiler, bool captured, bool edit)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() {Height = 20 });
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 40 });
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());
            //newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateGridFirstRow(name,spoiler, captured), 0, 0);
            newGrid.Children.Add(CreateLabelText(text), 0, 1);
            newGrid.Children.Add(CreateGridForReview(id, points, date), 0, 2);

            if(showspoilers == false && spoiler == true)
                newGrid.Children.Add(CreateLabelSpoilerHidden(text), 0, 1);

            if (edit == true) //prisijunges player atitinka komentaro kureja
            {
                newGrid.RowDefinitions.Add(new RowDefinition() { Height = 30 });
                newGrid.Children.Add(CreateGridForEditing(id), 0, 3);
            }


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

        private Grid CreateGridForReview(int id, int points, DateTime date)
        {
            Grid newGrid = new Grid();
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 120 });

            newGrid.Children.Add(CreateLabelText(points.ToString()), 0, 0);
            newGrid.Children.Add(CreateButtonUp(id), 1, 0);
            newGrid.Children.Add(CreateButtonDown(id), 2, 0);
            newGrid.Children.Add(CreateDateLabel(date), 3, 0);


            return newGrid;
        }

        private Grid CreateGridForEditing(int id)
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 155 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 70 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 70 });

            newGrid.Children.Add(DeleteButton(id), 1, 0);
            newGrid.Children.Add(EditButton(id), 2, 0);

            return newGrid;
        }

        private Button DeleteButton(int id)
        {
            Button button = new Button
            {
                Text = "DELETE",
                FontSize = 10,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#6CD4FF"),
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 70,
                HeightRequest = 30,
                CornerRadius = 30,
                ClassId = id.ToString(),
                
            };
            button.Clicked += async (sender, args) => DeleteFunc(sender, args);
            return button;
        }

        private async void DeleteFunc(object sender, EventArgs e)
        {
            bool action = await DisplayAlert("Deletion", "Are you sure you want to remove this comment?", "Yes", "Cancel");
            if (action)
            {
                var comment = await commentService.GetComment(Int16.Parse((sender as Button).ClassId));
                await commentService.DeleteComment(comment);
                CreateLayoutForMultipleComments();
            }
        }

        private Button EditButton(int id)
        {
            Button button = new Button
            {
                Text = "EDIT",
                FontSize = 10,
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#6CD4FF"),
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 70,
                HeightRequest = 30,
                CornerRadius = 30,
                ClassId = id.ToString()
            };
            button.Clicked += async (sender, args) => EditFunc(sender, args);
            return button;
        }

        private async void EditFunc(object sender, EventArgs e)
        {

            var comment = await commentService.GetComment(Int16.Parse((sender as Button).ClassId));
            await Navigation.PushPopupAsync(new EditCommentPage(commentService, comment));
            CreateLayoutForMultipleComments();
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

        private Image CreateButtonUp(int id)
        {
            Image image = new Image
            {
                Source = "green.png",
                ClassId = id.ToString()
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                RateComment(s, true);
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);

            return image;
        }

        private Image CreateButtonDown(int id)
        {
            Image image = new Image
            {
                Source = "red.png",
                ClassId = id.ToString()
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                RateComment(s, false);
            };
            image.GestureRecognizers.Add(tapGestureRecognizer);

            return image;
        }

        private async void RateComment(object sender, bool boolrating)
        {
            int id = Int16.Parse((sender as Image).ClassId);
            
            var ratings = await comratingService.GetCommentRatings();
            CommentRating rating;

            if (ratings == null || !ratings.Any())
            {
                rating = CreateCommentRating(id, boolrating);
                await comratingService.AddCommentRating(rating);
                //comment rating
                UpdateCommentRating(id, boolrating, 1);
                
                return;
            }

            //find old rating
            rating = ratings.SingleOrDefault(c => c.UserId == App.player.Id && c.LocationId == location.Id && c.CommentId == id);

            if (rating == null)
            {
                rating = CreateCommentRating(id, boolrating);
                await comratingService.AddCommentRating(rating);
                //comment rating
                UpdateCommentRating(id, boolrating, 1);
                
                return;
            }

            //update rating
            if (rating.Rating != boolrating)
            {
                rating.Rating = boolrating;
                await comratingService.UpdateCommentRating(rating);
                //comment rating
                UpdateCommentRating(id, boolrating, 2);
                
                return;
            }
            
        }

        private async void UpdateCommentRating(int id, bool boolrating, int num)
        {
            Comment comment = await commentService.GetComment(id);
            if (boolrating)
                comment.Rating = comment.Rating + num;
            else
                comment.Rating = comment.Rating - num;

            await commentService.UpdateComment(comment);
            CreateLayoutForMultipleComments();//maybe something smarter?
        }

            private CommentRating CreateCommentRating(int id, bool rating)
        {
            CommentRating newRating = new CommentRating();
            newRating.Rating = rating;
            newRating.CommentId = id;
            newRating.LocationId = location.Id;
            newRating.UserId = App.player.Id;

            return newRating;
        }


        private Label CreateDateLabel(DateTime date)
        {
            return new Label { Text = date.ToShortDateString(), TextColor = Color.Black, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
        }

        private Image CreateImage(String link, int userId)
        {
            Image img = new Image { Source = link, Margin = 5, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Start, ClassId = userId.ToString() };

            var openProfile = new TapGestureRecognizer();
            openProfile.Tapped += async (s, e) =>
            {
                OpenProfile(int.Parse((s as Image).ClassId));
            };
            img.GestureRecognizers.Add(openProfile);

            return img;
        }

        private async void OpenProfile(int userId)
        {
            var plrs = await playerService.GetPlayers();
            Player owner = null;

            foreach (var plr in plrs)
            {
                if (userId == plr.Id)
                {
                    owner = plr;
                    break;
                }
            }

            OpenProfilePage page = new OpenProfilePage(owner);
            page.GetInformation();
            await Navigation.PushPopupAsync(page);
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}