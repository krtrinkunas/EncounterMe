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
    public class BlockController : ControllerBase
    {
        private IBlockRepository _blockRepository;

        private readonly ILogger<BlockController> _logger;
        public BlockController(IBlockRepository blockRepository, ILogger<BlockController> logger)
        {
            _blockRepository = blockRepository;
            _logger = logger;
        }

        // GET: api/Blocks
        [HttpGet]
        public async Task<IEnumerable<Block>> GetBlocks()
        {
            return await _blockRepository.Get();
        }

        //GET: api/Blocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Block>> GetSingle(int id)
        {
            return await _blockRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Block>> Post([FromBody] Block value)
        {
            var newBlock = await _blockRepository.Create(value);
            return CreatedAtAction(nameof(GetBlocks), new { id = newBlock.Id }, newBlock);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Block>> Put(int id, [FromBody] Block value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }
            await _blockRepository.Update(value);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Block>> Delete(int id)
        {
            var blockToDelete = await _blockRepository.Get(id);
            if (blockToDelete == null)
            {
                return NotFound();
            }

            await _blockRepository.Delete(blockToDelete.Id);
            return NoContent();
        }
    }
}
