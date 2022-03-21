using EncounterMeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ModelContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<MyLocation> Locations {get; set;}
        public DbSet<Player> Players { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRating> CommentRatings { get; set; }
        public DbSet<LocationRating> LocationRatings { get; set; }
        public DbSet<CaptureAttempt> CaptureAttempts { get; set; }
    }
    public class YourDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlite("Data Source=EncounterMeDB.db");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
