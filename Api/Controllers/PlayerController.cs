using Api.Repositories;
using EncounterMeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayerRepository _playerRepository;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IPlayerRepository playerRepository, ILogger<PlayerController> logger)
        {
            _playerRepository = playerRepository;
            _logger = logger;
        }


        // GET: api/Players
        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
           return await _playerRepository.Get();      
        }

        // GET api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetSingle(int id)
        {
            return await _playerRepository.Get(id);
        }
        
        // POST api/Players
        [HttpPost]
        public async Task<ActionResult<Player>> Post([FromBody] Player value)
        {
            var newPlayer = await _playerRepository.Create(value);
            return CreatedAtAction(nameof(GetPlayers), new { id = newPlayer.Id }, newPlayer);
        }

        // PUT api/Players/5
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
            var playerToDelete = await _playerRepository.Get(id);
            if (playerToDelete == null)
            {
                return NotFound();
            }

            await _playerRepository.Delete(playerToDelete.Id);
            return NoContent();
        }

    }
}