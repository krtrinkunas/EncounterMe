using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ILocationRatingRepository
    {
        Task<IEnumerable<LocationRating>> Get();
        Task<LocationRating> Get(int id);
        Task<LocationRating> Create(LocationRating locationRating);
        Task Update(LocationRating locationRating);
        Task Delete(int id);

    }
}
