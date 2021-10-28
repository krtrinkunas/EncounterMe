using EncounterMeApp.Models;
using EncounterMeApp.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace EncounterMeApp.Services
{
    public static class LocationDatabase
    {
        static SQLiteAsyncConnection db;
        static async Task Init() //Initializing database
        {
            if (db != null)
                return;
            // var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LeaderboardDatabase.db"); //What is the difference?
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "LeaderboardDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<MapPage.Location>();
        }

        public static async Task AddLocation(Position pos, int point, string name, string own = "No owner")
        {
            await Init();
            var location = new MapPage.Location(pos, point, name, own);

            var id = await db.InsertAsync(location);
        }

        public static async Task RemoveLocation(int id)
        {
            await Init();

            await db.DeleteAsync<MapPage.Location>(id);
        }

        public static async Task<IEnumerable<MapPage.Location>> GetLocations()
        {
            await Init();

            var locations = await db.Table<MapPage.Location>().ToListAsync();
            return locations;
        }
    }
}
