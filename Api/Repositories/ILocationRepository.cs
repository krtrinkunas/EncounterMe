using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ILocationRepository
    {
        Task<IEnumerable<MyLocation>> Get();
        Task<MyLocation> Get(int id);
        Task<MyLocation> Create(MyLocation location);
        Task Update(MyLocation location);
        Task Delete(int id);
    
    }
}
