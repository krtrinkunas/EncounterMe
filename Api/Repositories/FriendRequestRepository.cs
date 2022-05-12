using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private DatabaseContext _context;

        public FriendRequestRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<FriendRequest> Create(FriendRequest friendRequest)
        {
            _context.Add(friendRequest);
            await _context.SaveChangesAsync();

            return friendRequest;
        }

        public async Task Delete(int id)
        {
            var friendRequestToDelete = await _context.FriendRequests.FindAsync(id);
            _context.FriendRequests.Remove(friendRequestToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FriendRequest>> Get()
        {
            return await _context.FriendRequests.ToListAsync();
        }

        public async Task<FriendRequest> Get(int id)
        {
            return await _context.FriendRequests.FindAsync(id);
        }

        public async Task Update(FriendRequest friendRequest)
        {
            _context.Entry(friendRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
