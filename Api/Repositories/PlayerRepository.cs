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
        private PlayerContext _context;

        public PlayerRepository(PlayerContext context)
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

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }
    }
}
