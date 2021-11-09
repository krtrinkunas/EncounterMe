using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface IPlayerService
    {

        Task<IEnumerable<Player>> GetPlayers();
        Task<Player> GetPlayer(int id);
        Task AddPlayer(Player player);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(Player player);
    }
}
