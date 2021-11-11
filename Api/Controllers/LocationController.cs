using Api.Repositories;
using EncounterMeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<IEnumerable<MyLocation>> GetLocations()
        {
            try
            {
                return await _locationRepository.Get();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in GetLocations, LocationController");
            }

            return null;
        }
        
        //GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyLocation>> GetSingle(int id)
        {
            try
            {
                return await _locationRepository.Get(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in GetSingle, LocationController");
            }

            return null;
        }

        [HttpPost]
        public async Task<ActionResult<MyLocation>> Post([FromBody] MyLocation value)
        {
            try
            {
                var newLocation = await _locationRepository.Create(value);
                return CreatedAtAction(nameof(GetLocations), new { id = newLocation.Id }, newLocation);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in Post, LocationController");
            }

            return null;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MyLocation>> Put(int id, [FromBody] MyLocation value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            try
            {
                await _locationRepository.Update(value);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in Put, LocationController");
            }

            return null;
            
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<MyLocation>> Delete(int id)
        {
            try
            {
                var locationToDelete = await _locationRepository.Get(id);
                if (locationToDelete == null)
                {
                    return NotFound();
                }

                await _locationRepository.Delete(locationToDelete.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in Delete, LocationController");
            }

            return null;
        }
    }
}
