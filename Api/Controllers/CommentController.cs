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
    public class CommentController : ControllerBase
    {
        private ICommentRepository _commentRepository;
        /*
        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();
        */
        private readonly ILogger<CommentController> _logger;
        public CommentController(ICommentRepository commentRepository, ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _commentRepository.Get();
        }

        //GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetSingle(int id)
        {
            return await _commentRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Post([FromBody] Comment value)
        {
            var newComment = await _commentRepository.Create(value);
            return CreatedAtAction(nameof(GetComments), new { id = newComment.CommentId }, newComment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> Put(int id, [FromBody] Comment value)
        {
            if (id != value.CommentId)
            {
                return BadRequest();
            }
            await _commentRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            var commentToDelete = await _commentRepository.Get(id);
            if (commentToDelete == null)
            {
                return NotFound();
            }

            await _commentRepository.Delete(commentToDelete.CommentId);
            return NoContent();
        }
    }
}
