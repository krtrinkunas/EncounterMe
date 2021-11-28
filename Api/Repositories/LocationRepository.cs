using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private DatabaseContext _context;

        public LocationRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<MyLocation> Create(MyLocation location)
        {
            _context.Add(location);
            await _context.SaveChangesAsync();

            return location;
        }

        public async Task Delete(int id)
        {
            var locationToDelete = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(locationToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyLocation>> Get()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<MyLocation> Get(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task Update(MyLocation location)
        {
            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
