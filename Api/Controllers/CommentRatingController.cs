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
    public class CommentRatingController : ControllerBase
    {
        private ICommentRatingRepository _commentRatingRepository;
        /*
        private static string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"log-{Date}.txt");

        ILogger logger = new LoggerConfiguration()
            .Enrich.WithExceptionDetails()
            .WriteTo.RollingFile(
            new JsonFormatter(renderMessage: true),
            file)
            .CreateLogger();
        */
        private readonly ILogger<CommentRatingController> _logger;
        public CommentRatingController(ICommentRatingRepository commentRatingRepository, ILogger<CommentRatingController> logger)
        {
            _commentRatingRepository = commentRatingRepository;
            _logger = logger;
        }

        // GET: api/CommentRatings
        [HttpGet]
        public async Task<IEnumerable<CommentRating>> GetCommentRatings()
        {
            return await _commentRatingRepository.Get();
        }

        //GET: api/CommentRatings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentRating>> GetSingle(int id)
        {
            return await _commentRatingRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<CommentRating>> Post([FromBody] CommentRating value)
        {
            var newCommentRating = await _commentRatingRepository.Create(value);
            return CreatedAtAction(nameof(GetCommentRatings), new { id = newCommentRating.CommentRatingId }, newCommentRating);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentRating>> Put(int id, [FromBody] CommentRating value)
        {
            if (id != value.CommentRatingId)
            {
                return BadRequest();
            }
            await _commentRatingRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentRating>> Delete(int id)
        {
            var commentRatingToDelete = await _commentRatingRepository.Get(id);
            if (commentRatingToDelete == null)
            {
                return NotFound();
            }

            await _commentRatingRepository.Delete(commentRatingToDelete.CommentRatingId);
            return NoContent();
        }
    }
}
