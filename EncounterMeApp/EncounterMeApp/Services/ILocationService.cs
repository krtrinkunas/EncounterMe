using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<MyLocation>> GetLocations();
        Task<MyLocation> GetLocation(int id);
        Task AddLocation(MyLocation location);
        Task UpdateLocation(MyLocation location);
        Task DeleteLocation(MyLocation location);
    }
}
