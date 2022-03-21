using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> Get();
        Task<Comment> Get(int id);
        Task<Comment> Create(Comment comment);
        Task Update(Comment comment);
        Task Delete(int id);

    }
}
