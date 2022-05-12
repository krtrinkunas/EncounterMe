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
    public class FriendRequestController : ControllerBase
    {
        private IFriendRequestRepository _friendRequestRepository;

        private readonly ILogger<FriendRequestController> _logger;
        public FriendRequestController(IFriendRequestRepository friendRequestRepository, ILogger<FriendRequestController> logger)
        {
            _friendRequestRepository = friendRequestRepository;
            _logger = logger;
        }

        // GET: api/FriendRequests
        [HttpGet]
        public async Task<IEnumerable<FriendRequest>> GetFriendRequests()
        {
            return await _friendRequestRepository.Get();
        }

        //GET: api/FriendRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendRequest>> GetSingle(int id)
        {
            return await _friendRequestRepository.Get(id);
        }

        [HttpGet("User/{id}")]
        public async Task<IEnumerable<FriendRequest>> GetFriendRequestsByUserID(int id)
        {
            return await _friendRequestRepository.GetByID(id);
        }

        [HttpPost]
        public async Task<ActionResult<FriendRequest>> Post([FromBody] FriendRequest value)
        {
            var newFriendRequest = await _friendRequestRepository.Create(value);
            return CreatedAtAction(nameof(GetFriendRequests), new { id = newFriendRequest.Id }, newFriendRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FriendRequest>> Put(int id, [FromBody] FriendRequest value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            await _friendRequestRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FriendRequest>> Delete(int id)
        {
            var friendRequestToDelete = await _friendRequestRepository.Get(id);
            if (friendRequestToDelete == null)
            {
                return NotFound();
            }

            await _friendRequestRepository.Delete(friendRequestToDelete.Id);
            return NoContent();
        }
    }
}
