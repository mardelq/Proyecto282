using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto282.Models;

namespace Proyecto282.Pages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfraestructurasController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public InfraestructurasController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/Infraestructuras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Infraestructura>>> GetInfraestructuras()
        {
          if (_context.Infraestructuras == null)
          {
              return NotFound();
          }
            return await _context.Infraestructuras.ToListAsync();
        }

        // GET: api/Infraestructuras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Infraestructura>> GetInfraestructura(int id)
        {
          if (_context.Infraestructuras == null)
          {
              return NotFound();
          }
            var infraestructura = await _context.Infraestructuras.FindAsync(id);

            if (infraestructura == null)
            {
                return NotFound();
            }

            return infraestructura;
        }

        // PUT: api/Infraestructuras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfraestructura(int id, Infraestructura infraestructura)
        {
            if (id != infraestructura.IdInfraestructura)
            {
                return BadRequest();
            }

            _context.Entry(infraestructura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfraestructuraExists(id))
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

        // POST: api/Infraestructuras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Infraestructura>> PostInfraestructura(Infraestructura infraestructura)
        {
          if (_context.Infraestructuras == null)
          {
              return Problem("Entity set 'Proyecto282Context.Infraestructuras'  is null.");
          }
            _context.Infraestructuras.Add(infraestructura);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InfraestructuraExists(infraestructura.IdInfraestructura))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInfraestructura", new { id = infraestructura.IdInfraestructura }, infraestructura);
        }

        // DELETE: api/Infraestructuras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfraestructura(int id)
        {
            if (_context.Infraestructuras == null)
            {
                return NotFound();
            }
            var infraestructura = await _context.Infraestructuras.FindAsync(id);
            if (infraestructura == null)
            {
                return NotFound();
            }

            _context.Infraestructuras.Remove(infraestructura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InfraestructuraExists(int id)
        {
            return (_context.Infraestructuras?.Any(e => e.IdInfraestructura == id)).GetValueOrDefault();
        }
    }
}
