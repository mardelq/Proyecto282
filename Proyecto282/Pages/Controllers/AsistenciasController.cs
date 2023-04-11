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
    public class AsistenciasController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public AsistenciasController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/Asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencia()
        {
          if (_context.Asistencia == null)
          {
              return NotFound();
          }
            return await _context.Asistencia.ToListAsync();
        }

        // GET: api/Asistencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistencia>> GetAsistencia(int id)
        {
          if (_context.Asistencia == null)
          {
              return NotFound();
          }
            var asistencia = await _context.Asistencia.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            return asistencia;
        }

        // PUT: api/Asistencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(int id, Asistencia asistencia)
        {
            if (id != asistencia.IdAsistencia)
            {
                return BadRequest();
            }

            _context.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
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

        // POST: api/Asistencias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asistencia>> PostAsistencia(Asistencia asistencia)
        {
          if (_context.Asistencia == null)
          {
              return Problem("Entity set 'Proyecto282Context.Asistencia'  is null.");
          }
            _context.Asistencia.Add(asistencia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AsistenciaExists(asistencia.IdAsistencia))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAsistencia", new { id = asistencia.IdAsistencia }, asistencia);
        }

        // DELETE: api/Asistencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(int id)
        {
            if (_context.Asistencia == null)
            {
                return NotFound();
            }
            var asistencia = await _context.Asistencia.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencia.Remove(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenciaExists(int id)
        {
            return (_context.Asistencia?.Any(e => e.IdAsistencia == id)).GetValueOrDefault();
        }
    }
}
