using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private DatabaseContext _context;

        public PlayerRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Player> Create(Player player)
        {
            _context.Add(player);
            await _context.SaveChangesAsync();

            return player;
        }

        public async Task Delete(int id)
        {
            var playerToDelete = await _context.Players.FindAsync(id);
            _context.Players.Remove(playerToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> Get()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> Get(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task Update(Player player)
        {
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
