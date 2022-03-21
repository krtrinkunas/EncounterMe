using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class CommentRatingRepository : ICommentRatingRepository
    {
        private DatabaseContext _context;

        public CommentRatingRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<CommentRating> Create(CommentRating commentRating)
        {
            _context.Add(commentRating);
            await _context.SaveChangesAsync();

            return commentRating;
        }

        public async Task Delete(int id)
        {
            var commentRatingToDelete = await _context.CommentRatings.FindAsync(id);
            _context.CommentRatings.Remove(commentRatingToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentRating>> Get()
        {
            return await _context.CommentRatings.ToListAsync();
        }

        public async Task<CommentRating> Get(int id)
        {
            return await _context.CommentRatings.FindAsync(id);
        }

        public async Task Update(CommentRating commentRating)
        {
            _context.Entry(commentRating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
