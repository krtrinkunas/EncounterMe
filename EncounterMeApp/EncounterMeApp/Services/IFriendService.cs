using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface IFriendService
    {
        Task<IEnumerable<Friend>> GetFriends();
        //Task<IEnumerable<Friend>> GetFriends(Player player);
        //Task<IEnumerable<Friend>> GetFriends(int playerID);
        Task<Friend> GetFriend(int id);
        Task AddFriend(Friend friend);
        Task UpdateFriend(Friend friend);
        Task DeleteFriend(Friend friend);
    }
}
