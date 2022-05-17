using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IFriendRequestRepository
    {
        Task<IEnumerable<FriendRequest>> Get();
        Task<FriendRequest> Get(int id);
        Task<IEnumerable<FriendRequest>> GetByID(int id);
        Task<FriendRequest> Create(FriendRequest friendRequest);
        Task Update(FriendRequest friendRequest);
        Task Delete(int id);

    }
}
