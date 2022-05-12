using Api.Repositories;
using EncounterMeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private IFriendRepository _friendRepository;

        private readonly ILogger<FriendController> _logger;
        public FriendController(IFriendRepository friendRepository, ILogger<FriendController> logger)
        {
            _friendRepository = friendRepository;
            _logger = logger;
        }

        // GET: api/Friends
        [HttpGet]
        public async Task<IEnumerable<Friend>> GetFriends()
        {
            return await _friendRepository.Get();
        }

        //GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> GetSingle(int id)
        {
            return await _friendRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Friend>> Post([FromBody] Friend value)
        {
            var newFriend = await _friendRepository.Create(value);
            return CreatedAtAction(nameof(GetFriends), new { id = newFriend.Id }, newFriend);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Friend>> Put(int id, [FromBody] Friend value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            await _friendRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Friend>> Delete(int id)
        {
            var friendToDelete = await _friendRepository.Get(id);
            if (friendToDelete == null)
            {
                return NotFound();
            }

            await _friendRepository.Delete(friendToDelete.Id);
            return NoContent();
        }
    }
}
