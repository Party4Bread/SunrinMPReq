using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SunrinMPReq.Models;

namespace SunrinMPReq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongReqController : ControllerBase
    {
        private readonly SunrinMPReqContext _context;

        public SongReqController(SunrinMPReqContext context)
        {
            _context = context;
        }

        // GET: api/SongReq
        [HttpGet]
        public IEnumerable<SongReqModel> GetSongReqModel()
        {
            return _context.SongReqModel;
        }

        // GET: api/SongReq/nM3e?roomkey=ghjh-mh-7568v-hikujm
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongReqModel([FromRoute] string id,[FromQuery] string roomkey)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songReqModel = _context.SongReqModel.Where(x=>x.RoomId==id);

            if (songReqModel == null)
            {
                return NotFound();
            }

            return Ok(songReqModel);
        }

        // PUT: api/SongReq/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongReqModel([FromRoute] int id, [FromBody] SongReqModel songReqModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != songReqModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(songReqModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongReqModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SongReq
        [HttpPost]
        public async Task<IActionResult> PostSongReqModel([FromForm] SongReqModel songReqModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SongReqModel.Add(songReqModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSongReqModel", new { id = songReqModel.Id }, songReqModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongReqModel([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songReqModel = await _context.SongReqModel.FirstAsync(x => x.RoomId==id);
            if (songReqModel == null)
            {
                return NotFound();
            }

            _context.SongReqModel.Remove(songReqModel);
            await _context.SaveChangesAsync();

            return Ok(songReqModel);
        }
        // DELETE: api/SongReq/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSongReqModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var songReqModel = await _context.SongReqModel.FindAsync(id);
            if (songReqModel == null)
            {
                return NotFound();
            }

            _context.SongReqModel.Remove(songReqModel);
            await _context.SaveChangesAsync();

            return Ok(songReqModel);
        }
        */

        private bool SongReqModelExists(int id)
        {
            return _context.SongReqModel.Any(e => e.Id == id);
        }
    }
}