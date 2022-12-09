using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonApiNew.Models;

namespace SalonApiNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientRecordsController : ControllerBase
    {
        private readonly BeatySalonApiContext _context;

        public ClientRecordsController(BeatySalonApiContext context)
        {
            _context = context;
        }

        // GET: api/ClientRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientRecords>>> GetClientRecords()
        {
            return await _context.ClientRecords.ToListAsync();
        }

        // GET: api/ClientRecords/5
        [HttpGet("{date}/{time}")]
        public async Task<ActionResult<ClientRecords>> GetClientRecords(string date, string time)
        {
            var clientRecords = await _context.ClientRecords.FirstOrDefaultAsync(x => x.RecordDate == date && x.RecordTime == time);

            if (clientRecords == null)
            {
                return NotFound();
            }

            return clientRecords;
        }

        // PUT: api/ClientRecords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientRecords(int id, ClientRecords clientRecords)
        {
            if (id != clientRecords.IdRecord)
            {
                return BadRequest();
            }

            _context.Entry(clientRecords).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientRecordsExists(id))
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

        // POST: api/ClientRecords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ClientRecords>> PostClientRecords(ClientRecords clientRecords)
        {
            _context.ClientRecords.Add(clientRecords);
            try
            {
                await _context.SaveChangesAsync();
                return clientRecords;
            }
            catch (DbUpdateException)
            {
                if (ClientRecordsExists(clientRecords.IdRecord))
                {
                    return BadRequest();
                }
                
            }

            return CreatedAtAction("GetClientRecords", new { id = clientRecords.IdRecord }, clientRecords);
        }

        // DELETE: api/ClientRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientRecords>> DeleteClientRecords(int id)
        {
            var clientRecords = await _context.ClientRecords.FindAsync(id);
            if (clientRecords == null)
            {
                return NotFound();
            }

            _context.ClientRecords.Remove(clientRecords);
            await _context.SaveChangesAsync();

            return clientRecords;
        }

        private bool ClientRecordsExists(int id)
        {
            return _context.ClientRecords.Any(e => e.IdRecord == id);
        }
    }
}
