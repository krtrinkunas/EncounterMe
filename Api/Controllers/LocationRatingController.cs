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
    public class LocationRatingController : ControllerBase
    {
        private ILocationRatingRepository _locationRatingRepository;
        /*
        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();
        */
        private readonly ILogger<LocationRatingController> _logger;
        public LocationRatingController(ILocationRatingRepository locationRatingRepository, ILogger<LocationRatingController> logger)
        {
            _locationRatingRepository = locationRatingRepository;
            _logger = logger;
        }

        // GET: api/LocationRatings
        [HttpGet]
        public async Task<IEnumerable<LocationRating>> GetLocationRatings()
        {
            return await _locationRatingRepository.Get();
        }

        //GET: api/LocationRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationRating>> GetSingle(int id)
        {
            return await _locationRatingRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<LocationRating>> Post([FromBody] LocationRating value)
        {
            var newLocationRating = await _locationRatingRepository.Create(value);
            return CreatedAtAction(nameof(GetLocationRatings), new { id = newLocationRating.LocationRatingId }, newLocationRating);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LocationRating>> Put(int id, [FromBody] LocationRating value)
        {
            if (id != value.LocationRatingId)
            {
                return BadRequest();
            }
            await _locationRatingRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LocationRating>> Delete(int id)
        {
            var locationRatingToDelete = await _locationRatingRepository.Get(id);
            if (locationRatingToDelete == null)
            {
                return NotFound();
            }

            await _locationRatingRepository.Delete(locationRatingToDelete.LocationRatingId);
            return NoContent();
        }
    }
}
