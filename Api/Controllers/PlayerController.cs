using Api.Repositories;
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
        private IPlayerRepository _playerRepository;

        //public static List<Player> Players { get; } = new List<Player>();


        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        // GET: api/Player
        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            try
            {
                return await _playerRepository.Get();
            }
            catch (Exception ex)
            {
               
            }

            return null;
            
        }

        // GET api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetSingle(int id)
        {
            return await _playerRepository.Get(id);
        }
        
        // POST api/Player
        [HttpPost]
        public async Task<ActionResult<Player>> Post([FromBody] Player value)
        {
            try
            {
                var newPlayer = await _playerRepository.Create(value);
                return CreatedAtAction(nameof(GetPlayers), new { id = newPlayer.Id }, newPlayer);
            }
            catch(Exception ex)
            {

            }

            return null;
        }

        // PUT api/Player/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Player>> Put(int id, [FromBody] Player value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }

            await _playerRepository.Update(value);
            return NoContent();
        }


        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> Delete(int id)
        {
            try
            {
                var playerToDelete = await _playerRepository.Get(id);
                if (playerToDelete == null)
                {
                    return NotFound();
                }

                await _playerRepository.Delete(playerToDelete.Id);
                return NoContent();
            }
            catch (Exception ex)
            {

            }

            return null;
        }

    }
}