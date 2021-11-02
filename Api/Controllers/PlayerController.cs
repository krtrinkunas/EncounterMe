using EncounterMeApp.Models;
using Microsoft.AspNetCore.Mvc;
//using MyCoffeeApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Api.PlayerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        public static List<Player> Players { get; } = new List<Player>();
        // GET: api/Player
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return Players;
        }

        // GET api/Player/5
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            return Players.FirstOrDefault(c => c.Id == id);
        }

        // POST api/Player
        [HttpPost]
        public void Post([FromBody] Player value)
        {
            Players.Add(value);
        }

        // PUT api/Player/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Player value)
        {
            var coffee = Players.FirstOrDefault(c => c.Id == id);
            if (coffee == null)
                return;

            coffee = value;
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var coffee = Players.FirstOrDefault(c => c.Id == id);
            if (coffee == null)
                return;

            Players.Remove(coffee);
        }
    }
}