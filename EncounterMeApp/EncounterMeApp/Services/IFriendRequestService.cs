using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface IFriendRequestService
    {
        Task<IEnumerable<FriendRequest>> GetFriendRequests();
        //Task<IEnumerable<FriendRequest>> GetFriendRequests(Player player);
        Task<IEnumerable<FriendRequest>> GetFriendRequests(int id);
        Task<FriendRequest> GetFriendRequest(int id);
        Task AddFriendRequest(FriendRequest friendRequest);
        Task UpdateFriendRequest(FriendRequest friendRequest);
        Task DeleteFriendRequest(FriendRequest friendRequest);
    }
}
