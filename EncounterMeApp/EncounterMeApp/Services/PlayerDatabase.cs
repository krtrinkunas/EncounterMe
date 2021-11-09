using EncounterMeApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EncounterMeApp.Services
{
    public static class PlayerDatabase
    {
        static SQLiteAsyncConnection db;
        static async Task Init() //Initializing database
        {
            if (db != null)
                return;
            // var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LeaderboardDatabase.db"); //What is the difference?
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "LeaderboardDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Player>();
        }

        public static async Task AddPlayer(string nickName, int points, string firstname, string lastname, string password, string email)
        {
            await Init();
            var image = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png";
            var player = new Player
            {
                NickName = nickName,
                Points = points,
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Password = password,
                ProfilePic = image
            };

            var id = await db.InsertAsync(player);
        }

        public static async Task RemovePlayer(int id)
        {
            await Init();

            await db.DeleteAsync<Player>(id);
        }

        public static async Task<IEnumerable<Player>> GetPlayers()
        {
            await Init();

            var players = await db.Table<Player>().ToListAsync();
            return players;
        }
    }
}
