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
            // var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LocationDatabase.db"); //What is the difference?
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "LocationDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<MyLocation>();
        }

        public static async Task AddLocation(double posX, double posY, int point, string name, string own = "No owner")
        {
            await Init();
            var location = new MyLocation
            {
                positionX = posX,
                positionY = posY,
                NAME = name,
                points = point,
                owner = own
            };

            var id = await db.InsertAsync(location);
        }

        public static async Task RemoveLocation(int id)
        {
            await Init();

            await db.DeleteAsync<MyLocation>(id);
        }

        public static async Task<IEnumerable<MyLocation>> GetLocations()
        {
            await Init();

            var locations = await db.Table<MyLocation>().ToListAsync();
            return locations;
        }
    }
}
