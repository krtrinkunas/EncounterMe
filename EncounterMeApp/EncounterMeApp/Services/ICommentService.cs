using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetComments();
        Task<Comment> GetComment(int id);
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Comment comment);
    }
}
