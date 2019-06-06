using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedWebApp.Models;

namespace MedWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientsWpfController : ControllerBase
    {
        private readonly PecientContext _context;

        public PacientsWpfController(PecientContext context)
        {
            _context = context;
        }

        // GET: api/PacientsWpf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pacient>>> GetPacients()
        {
            return await _context.Pacients.ToListAsync();
        }

        // GET: api/PacientsWpf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pacient>> GetPacient(int id)
        {
            var pacient = await _context.Pacients.FindAsync(id);

            if (pacient == null)
            {
                return NotFound();
            }

            return pacient;
        }

        // PUT: api/PacientsWpf/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacient(int id, Pacient pacient)
        {
            if (id != pacient.Id)
            {
                return BadRequest();
            }

            _context.Entry(pacient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientExists(id))
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

        // POST: api/PacientsWpf
        [HttpPost]
        public async Task<ActionResult<Pacient>> PostPacient(Pacient pacient)
        {
            _context.Pacients.Add(pacient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPacient", new { id = pacient.Id }, pacient);
        }

        // DELETE: api/PacientsWpf/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pacient>> DeletePacient(int id)
        {
            var pacient = await _context.Pacients.FindAsync(id);
            if (pacient == null)
            {
                return NotFound();
            }

            _context.Pacients.Remove(pacient);
            await _context.SaveChangesAsync();

            return pacient;
        }

        private bool PacientExists(int id)
        {
            return _context.Pacients.Any(e => e.Id == id);
        }
    }
}
