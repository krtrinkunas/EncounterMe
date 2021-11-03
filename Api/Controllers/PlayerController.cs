using EncounterMeApp.Models;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

//Use db instead of collection
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        //public static List<Player> Players { get; } = new List<Player>();

        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db != null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PlayerDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Player>();
        }

        // GET: api/Player
        [HttpGet]
        public async Task<IEnumerable<Player>> Get()
        {
            try
            {
                await Init();
                var players = await db.Table<Player>().ToListAsync();
                return players;
            }
            catch(Exception ex)
            {
            }

            return null;
            
        }

        /*
        // GET api/Player/5
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            return Players.FirstOrDefault(c => c.Id == id);
        }
        */

        // POST api/Player
        [HttpPost]
        public async void Post([FromBody] Player value)
        {
            try
            {
                await Init();
                await db.InsertAsync(value);
            }
            catch(Exception ex)
            {

            }

            //Players.Add(value);
        }

        /*
        // PUT api/Player/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Player value)
        {
            var coffee = await db.(c => c.Id == id);
            if (coffee == null)
                return;

            coffee = value;
        }
        */

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            try
            {
                await Init();
                await db.DeleteAsync<Player>(id);
            }
            catch (Exception ex)
            {

            }
            /*
            var coffee = Players.FirstOrDefault(c => c.Id == id);
            if (coffee == null)
                return;

            Players.Remove(coffee);
            */
        }
    }
}