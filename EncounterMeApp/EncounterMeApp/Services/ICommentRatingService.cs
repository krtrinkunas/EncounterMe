using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface ICommentRatingService
    {
        Task<IEnumerable<CommentRating>> GetCommentRatings();
        Task<CommentRating> GetCommentRating(int id);
        Task AddCommentRating(CommentRating commentRating);
        Task UpdateCommentRating(CommentRating commentRating);
        Task DeleteCommentRating(CommentRating commentRating);
    }
}
