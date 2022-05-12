using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IFriendRepository
    {
        Task<IEnumerable<Friend>> Get();
        Task<Friend> Get(int id);
        Task<Friend> Create(Friend friend);
        Task Update(Friend friend);
        Task Delete(int id);

    }
}
