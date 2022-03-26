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
using EncounterMeApp.Services;
using EncounterMeApp.Models;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCommentPage : PopupPage
    {
        ICommentService commentService;
        Comment comment;

        public EditCommentPage(ICommentService commentService, Comment comment)
        {
            InitializeComponent();
            this.commentService = commentService;
            this.comment = comment;
            entryComment.Text = comment.CommentText;
        }

        private async void GoBack(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        private async void EditComment(object sender, EventArgs e)
        {
            comment.CommentText = entryComment.Text;
            await commentService.UpdateComment(comment);
            await Navigation.PopPopupAsync();
        }

        private async void CancelEdit(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}