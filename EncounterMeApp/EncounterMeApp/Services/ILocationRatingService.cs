using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface ILocationRatingService
    {
        Task<IEnumerable<LocationRating>> GetLocationRatings();
        Task<LocationRating> GetLocationRating(int id);
        Task AddLocationRating(LocationRating locationRating);
        Task UpdateLocationRating(LocationRating locationRating);
        Task DeleteLocationRating(LocationRating locationRating);
    }
}
