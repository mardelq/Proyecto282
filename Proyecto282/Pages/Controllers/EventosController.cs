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
    public class EventosController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public EventosController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
          if (_context.Eventos == null)
          {
              return NotFound();
          }
            return await _context.Eventos.ToListAsync();
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
          if (_context.Eventos == null)
          {
              return NotFound();
          }
            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return evento;
        }

        // PUT: api/Eventos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Eventos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento evento)
        {
          if (_context.Eventos == null)
          {
              return Problem("Entity set 'Proyecto282Context.Eventos'  is null.");
          }
            _context.Eventos.Add(evento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventoExists(evento.IdEvento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEvento", new { id = evento.IdEvento }, evento);
        }

        // DELETE: api/Eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            if (_context.Eventos == null)
            {
                return NotFound();
            }
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return (_context.Eventos?.Any(e => e.IdEvento == id)).GetValueOrDefault();
        }
    }
}
