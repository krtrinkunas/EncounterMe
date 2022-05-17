using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private DatabaseContext _context;

        public FriendRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Friend> Create(Friend friend)
        {
            _context.Add(friend);
            await _context.SaveChangesAsync();

            return friend;
        }

        public async Task Delete(int id)
        {
            var friendToDelete = await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friendToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Friend>> Get()
        {
            return await _context.Friends.ToListAsync();
        }

        public async Task<Friend> Get(int id)
        {
            return await _context.Friends.FindAsync(id);
        }

        public async Task Update(Friend friend)
        {
            _context.Entry(friend).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Friend>> GetByID(int userID)
        {
            return await _context.Friends.Where( user => user.Friend1ID == userID || user.Friend2ID == userID).ToListAsync();
        }
    }
}
