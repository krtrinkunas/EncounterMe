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
    public class LocationController : ControllerBase
    {
        private ILocationRepository _locationRepository;
        /*
        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();
        */
        private readonly ILogger<LocationController> _logger;
        public LocationController(ILocationRepository locationRepository, ILogger<LocationController> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<IEnumerable<MyLocation>> GetLocations()
        {
           return await _locationRepository.Get();
        }
        
        //GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyLocation>> GetSingle(int id)
        {
            return await _locationRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<MyLocation>> Post([FromBody] MyLocation value)
        {
            var newLocation = await _locationRepository.Create(value);
            return CreatedAtAction(nameof(GetLocations), new { id = newLocation.Id }, newLocation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MyLocation>> Put(int id, [FromBody] MyLocation value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            await _locationRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<MyLocation>> Delete(int id)
        {
            var locationToDelete = await _locationRepository.Get(id);
            if (locationToDelete == null)
            {
                return NotFound();
            }

            await _locationRepository.Delete(locationToDelete.Id);
            return NoContent();
        }
    }
}
