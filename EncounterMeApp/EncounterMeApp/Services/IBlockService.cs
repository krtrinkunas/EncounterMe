using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface IBlockService
    {
        Task<IEnumerable<Block>> GetBlocks();
        Task<Block> GetBlock(int id);
        Task AddBlock(Block block);
        Task UpdateBlock(Block block);
        Task DeleteBlock(Block block);
    }
}
