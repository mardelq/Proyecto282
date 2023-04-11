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
    public class RecursosController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public RecursosController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/Recursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recurso>>> GetRecursos()
        {
          if (_context.Recursos == null)
          {
              return NotFound();
          }
            return await _context.Recursos.ToListAsync();
        }

        // GET: api/Recursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recurso>> GetRecurso(int id)
        {
          if (_context.Recursos == null)
          {
              return NotFound();
          }
            var recurso = await _context.Recursos.FindAsync(id);

            if (recurso == null)
            {
                return NotFound();
            }

            return recurso;
        }

        // PUT: api/Recursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecurso(int id, Recurso recurso)
        {
            if (id != recurso.IdRecurso)
            {
                return BadRequest();
            }

            _context.Entry(recurso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursoExists(id))
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

        // POST: api/Recursos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recurso>> PostRecurso(Recurso recurso)
        {
          if (_context.Recursos == null)
          {
              return Problem("Entity set 'Proyecto282Context.Recursos'  is null.");
          }
            _context.Recursos.Add(recurso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RecursoExists(recurso.IdRecurso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRecurso", new { id = recurso.IdRecurso }, recurso);
        }

        // DELETE: api/Recursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecurso(int id)
        {
            if (_context.Recursos == null)
            {
                return NotFound();
            }
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }

            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecursoExists(int id)
        {
            return (_context.Recursos?.Any(e => e.IdRecurso == id)).GetValueOrDefault();
        }
    }
}
