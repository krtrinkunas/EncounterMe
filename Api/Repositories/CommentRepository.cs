using Api.ModelContext;
using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private DatabaseContext _context;

        public CommentRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Comment> Create(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task Delete(int id)
        {
            var commentToDelete = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> Get()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> Get(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task Update(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
