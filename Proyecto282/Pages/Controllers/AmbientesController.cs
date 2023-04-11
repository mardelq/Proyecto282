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
    public class AmbientesController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public AmbientesController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/Ambientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ambiente>>> GetAmbientes()
        {
          if (_context.Ambientes == null)
          {
              return NotFound();
          }
            return await _context.Ambientes.ToListAsync();
        }

        // GET: api/Ambientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ambiente>> GetAmbiente(int id)
        {
          if (_context.Ambientes == null)
          {
              return NotFound();
          }
            var ambiente = await _context.Ambientes.FindAsync(id);

            if (ambiente == null)
            {
                return NotFound();
            }

            return ambiente;
        }

        // PUT: api/Ambientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmbiente(int id, Ambiente ambiente)
        {
            if (id != ambiente.IdAmbiente)
            {
                return BadRequest();
            }

            _context.Entry(ambiente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmbienteExists(id))
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

        // POST: api/Ambientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ambiente>> PostAmbiente(Ambiente ambiente)
        {
          if (_context.Ambientes == null)
          {
              return Problem("Entity set 'Proyecto282Context.Ambientes'  is null.");
          }
            _context.Ambientes.Add(ambiente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AmbienteExists(ambiente.IdAmbiente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAmbiente", new { id = ambiente.IdAmbiente }, ambiente);
        }

        // DELETE: api/Ambientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmbiente(int id)
        {
            if (_context.Ambientes == null)
            {
                return NotFound();
            }
            var ambiente = await _context.Ambientes.FindAsync(id);
            if (ambiente == null)
            {
                return NotFound();
            }

            _context.Ambientes.Remove(ambiente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmbienteExists(int id)
        {
            return (_context.Ambientes?.Any(e => e.IdAmbiente == id)).GetValueOrDefault();
        }
    }
}
