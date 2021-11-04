using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayers();
        //Task<Player> GetSingle(int id);
        Task<Player> Create(Player player);
        //Task Update(Player player);
        Task Delete(int id);
        

    }
}
