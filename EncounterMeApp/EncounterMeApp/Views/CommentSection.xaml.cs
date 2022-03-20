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
       /* MainPage main;
        List<EncounterMe.Classes.Attribute> pickedAttributes;
        List<EncounterMe.Classes.Attribute> attributes;*/

        public CommentSection()
        {
            
            InitializeComponent();
            //gridLayout = CreateGridForComment();
           // test = new Label { Text = "wtf???" };

            /*this.main = main;
            pickedAttributes = main.pickedAttributes;
            attributes = main.attributes;
            CreateGrid(3, attributes);*/
        }
/*
        private void CreateGrid(int columnNum, List<EncounterMe.Classes.Attribute> attributeList)
        {
            gridLayout.RowDefinitions.Add(new RowDefinition());
            for (int i = 0; i < columnNum; i++)
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            FillGridWithElements(gridLayout, columnNum, attributeList);
        }

        private void FillGridWithElements(Grid grid, int columnNum, List<EncounterMe.Classes.Attribute> attributeList)
        {
            int column = -1;
            int row = 0;
            foreach (var attribute in attributeList)
            {
                //add grid and Image in stacklayout
                StackLayout stackLayout = new StackLayout
                {
                    Children =
                    {
                        CreateGridForAttribute(attribute),
                        new Image {Source = attribute.Image }
                    }
                };

                //assign to proper place in whole grid
                column++;
                if (column == columnNum)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    column = 0;
                    row++;
                }
                grid.Children.Add(stackLayout, column, row);
            }
        }
*/
        public void CreateGridForComment()
        {
            Grid newGrid = new Grid();
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition() {Width = 70 });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            gridLayout.Children.Add(CreateImage("discover_button.png"), 0, 0);
            gridLayout.Children.Add(CreateGridForCommentInfo(), 1, 0); //column, row
            //return newGrid;
        }

        private Grid CreateGridForCommentInfo()
        {
            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() {Height = 20 });
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = 40 });
            newGrid.RowDefinitions.Add(new RowDefinition());
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            newGrid.Children.Add(CreateLabelName("HamsterWasHere"), 0, 0);
            newGrid.Children.Add(CreateLabelText("Prasau veik Prasau veik Prasau veik"), 0, 1);
            newGrid.Children.Add(CreateGridForReview(), 0, 2);

            return newGrid;
        }

        private Grid CreateGridForReview()
        {
            Grid newGrid = new Grid();
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 30 });

            newGrid.Children.Add(CreateLabelText("10"), 0, 0);
            newGrid.Children.Add(CreateButtonUp(), 1, 0);
            newGrid.Children.Add(CreateButtonDown(), 2, 0);

            return newGrid;
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
/*
        private CheckBox CreateCheckBoxFromAttribute(EncounterMe.Classes.Attribute attribute)
        {
            CheckBox newCheckBox = new CheckBox()
            {
                ClassId = attribute.Name,
                BackgroundColor = Color.Default,
                IsChecked = pickedAttributes.Contains(attribute),
            };
            newCheckBox.CheckedChanged += CheckedAttribute;

            return newCheckBox;
        }
*/
        private async void GoBack(object sender, EventArgs e)
        {
            //main.pickedAttributes = pickedAttributes;
            await Navigation.PopAsync();
            //main.PopupSearchEncounter(sender, e);
        }
/*
        private void CheckedAttribute(object sender, EventArgs e)
        {
            String attributeName = (sender as CheckBox).ClassId;
            var attribute = pickedAttributes.Find(e => e.Name == attributeName);
            if (attribute == null)
                pickedAttributes.Add(attributes.Find(e => e.Name == attributeName));
            else
                pickedAttributes.Remove(attribute);
        }
*/
    }
}