using Microsoft.EntityFrameworkCore;
using Api.ModelContext;

namespace AppTests.DatabaseIntegrationTests
{
    public class DatabaseIntegrationTestsFixture
    {
        private const string ConnectionString = "Data source=EncounterMeDB.db";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

    public DatabaseIntegrationTestsFixture ()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }
        public DatabaseContext CreateContext()
        => new DatabaseContext(
            new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(ConnectionString)
                .Options);
    }
}
