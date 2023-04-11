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
    public class HorarioExposicionesController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public HorarioExposicionesController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/HorarioExposiciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioExposicion>>> GetHorarioExposicions()
        {
          if (_context.HorarioExposicions == null)
          {
              return NotFound();
          }
            return await _context.HorarioExposicions.ToListAsync();
        }

        // GET: api/HorarioExposiciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioExposicion>> GetHorarioExposicion(int id)
        {
          if (_context.HorarioExposicions == null)
          {
              return NotFound();
          }
            var horarioExposicion = await _context.HorarioExposicions.FindAsync(id);

            if (horarioExposicion == null)
            {
                return NotFound();
            }

            return horarioExposicion;
        }

        // PUT: api/HorarioExposiciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioExposicion(int id, HorarioExposicion horarioExposicion)
        {
            if (id != horarioExposicion.IdHorarioExposicion)
            {
                return BadRequest();
            }

            _context.Entry(horarioExposicion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExposicionExists(id))
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

        // POST: api/HorarioExposiciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HorarioExposicion>> PostHorarioExposicion(HorarioExposicion horarioExposicion)
        {
          if (_context.HorarioExposicions == null)
          {
              return Problem("Entity set 'Proyecto282Context.HorarioExposicions'  is null.");
          }
            _context.HorarioExposicions.Add(horarioExposicion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HorarioExposicionExists(horarioExposicion.IdHorarioExposicion))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHorarioExposicion", new { id = horarioExposicion.IdHorarioExposicion }, horarioExposicion);
        }

        // DELETE: api/HorarioExposiciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioExposicion(int id)
        {
            if (_context.HorarioExposicions == null)
            {
                return NotFound();
            }
            var horarioExposicion = await _context.HorarioExposicions.FindAsync(id);
            if (horarioExposicion == null)
            {
                return NotFound();
            }

            _context.HorarioExposicions.Remove(horarioExposicion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioExposicionExists(int id)
        {
            return (_context.HorarioExposicions?.Any(e => e.IdHorarioExposicion == id)).GetValueOrDefault();
        }
    }
}
