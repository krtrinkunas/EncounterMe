using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class CaptureAttemptRepository : ICaptureAttemptRepository
    {
        private DatabaseContext _context;

        public CaptureAttemptRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<CaptureAttempt> Create(CaptureAttempt captureAttempt)
        {
            _context.Add(captureAttempt);
            await _context.SaveChangesAsync();

            return captureAttempt;
        }

        public async Task Delete(int id)
        {
            var captureAttemptToDelete = await _context.CaptureAttempts.FindAsync(id);
            _context.CaptureAttempts.Remove(captureAttemptToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CaptureAttempt>> Get()
        {
            return await _context.CaptureAttempts.ToListAsync();
        }

        public async Task<CaptureAttempt> Get(int id)
        {
            return await _context.CaptureAttempts.FindAsync(id);
        }

        public async Task Update(CaptureAttempt captureAttempt)
        {
            _context.Entry(captureAttempt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
