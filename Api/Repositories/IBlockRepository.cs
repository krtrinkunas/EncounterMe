using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IBlockRepository
    {
        Task<IEnumerable<Block>> Get();
        Task<Block> Get(int id);
        Task<Block> Create(Block block);
        Task Update(Block block);
        Task Delete(int id);

    }
}
