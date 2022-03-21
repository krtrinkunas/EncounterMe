using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICommentRatingRepository
    {
        Task<IEnumerable<CommentRating>> Get();
        Task<CommentRating> Get(int id);
        Task<CommentRating> Create(CommentRating commentRating);
        Task Update(CommentRating commentRating);
        Task Delete(int id);

    }
}
