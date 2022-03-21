using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class LocationRatingRepository : ILocationRatingRepository
    {
        private DatabaseContext _context;

        public LocationRatingRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<LocationRating> Create(LocationRating locationRating)
        {
            _context.Add(locationRating);
            await _context.SaveChangesAsync();

            return locationRating;
        }

        public async Task Delete(int id)
        {
            var locationRatingToDelete = await _context.LocationRatings.FindAsync(id);
            _context.LocationRatings.Remove(locationRatingToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LocationRating>> Get()
        {
            return await _context.LocationRatings.ToListAsync();
        }

        public async Task<LocationRating> Get(int id)
        {
            return await _context.LocationRatings.FindAsync(id);
        }

        public async Task Update(LocationRating locationRating)
        {
            _context.Entry(locationRating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
