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
    public class CaptureAttemptController : ControllerBase
    {
        private ICaptureAttemptRepository _captureAttemptRepository;
        /*
        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();
        */
        private readonly ILogger<CaptureAttemptController> _logger;
        public CaptureAttemptController(ICaptureAttemptRepository captureAttemptRepository, ILogger<CaptureAttemptController> logger)
        {
            _captureAttemptRepository = captureAttemptRepository;
            _logger = logger;
        }

        // GET: api/CaptureAttempts
        [HttpGet]
        public async Task<IEnumerable<CaptureAttempt>> GetCaptureAttempts()
        {
            return await _captureAttemptRepository.Get();
        }

        //GET: api/CaptureAttempts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaptureAttempt>> GetSingle(int id)
        {
            return await _captureAttemptRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<CaptureAttempt>> Post([FromBody] CaptureAttempt value)
        {
            var newCaptureAttempt = await _captureAttemptRepository.Create(value);
            return CreatedAtAction(nameof(GetCaptureAttempts), new { id = newCaptureAttempt.CaptureAttemptId }, newCaptureAttempt);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CaptureAttempt>> Put(int id, [FromBody] CaptureAttempt value)
        {
            if (id != value.CaptureAttemptId)
            {
                return BadRequest();
            }
            await _captureAttemptRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CaptureAttempt>> Delete(int id)
        {
            var captureAttemptToDelete = await _captureAttemptRepository.Get(id);
            if (captureAttemptToDelete == null)
            {
                return NotFound();
            }

            await _captureAttemptRepository.Delete(captureAttemptToDelete.CaptureAttemptId);
            return NoContent();
        }
    }
}
