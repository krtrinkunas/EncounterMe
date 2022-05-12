using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private DatabaseContext _context;

        public BlockRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Block> Create(Block block)
        {
            _context.Add(block);
            await _context.SaveChangesAsync();

            return block;
        }

        public async Task Delete(int id)
        {
            var blockToDelete = await _context.Blocks.FindAsync(id);
            _context.Blocks.Remove(blockToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Block>> Get()
        {
            return await _context.Blocks.ToListAsync();
        }

        public async Task<Block> Get(int id)
        {
            return await _context.Blocks.FindAsync(id);
        }

        public async Task Update(Block block)
        {
            _context.Entry(block).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
