using Api.Repositories;
using EncounterMeApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        // GET: api/Players
        [HttpGet]
        public async Task<IEnumerable<Player>> GetPlayers()
        {
            try
            {
                return await _playerRepository.Get();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in GetPlayers, PlayerController");
            }

            return null;
            
        }

        // GET api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetSingle(int id)
        {
            try
            {
                return await _playerRepository.Get(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in GetSingle, PlayerController");
            }

            return null;
        }
        
        // POST api/Players
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
                logger.Error(ex, "Exception in Post, PlayerController");
            }

            return null;
        }

        // PUT api/Players/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Player>> Put(int id, [FromBody] Player value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }

            try
            {
                await _playerRepository.Update(value);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in Put, PlayerController");
            }

            return null;
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
                logger.Error(ex, "Exception in Delete, PlayerController");
            }

            return null;
        }

    }
}