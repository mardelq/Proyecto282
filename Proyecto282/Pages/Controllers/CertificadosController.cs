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
    public class CertificadosController : ControllerBase
    {
        private readonly Proyecto282Context _context;

        public CertificadosController(Proyecto282Context context)
        {
            _context = context;
        }

        // GET: api/Certificados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificado>>> GetCertificados()
        {
          if (_context.Certificados == null)
          {
              return NotFound();
          }
            return await _context.Certificados.ToListAsync();
        }

        // GET: api/Certificados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> GetCertificado(int id)
        {
          if (_context.Certificados == null)
          {
              return NotFound();
          }
            var certificado = await _context.Certificados.FindAsync(id);

            if (certificado == null)
            {
                return NotFound();
            }

            return certificado;
        }

        // PUT: api/Certificados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificado(int id, Certificado certificado)
        {
            if (id != certificado.IdCertificado)
            {
                return BadRequest();
            }

            _context.Entry(certificado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificadoExists(id))
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

        // POST: api/Certificados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Certificado>> PostCertificado(Certificado certificado)
        {
          if (_context.Certificados == null)
          {
              return Problem("Entity set 'Proyecto282Context.Certificados'  is null.");
          }
            _context.Certificados.Add(certificado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CertificadoExists(certificado.IdCertificado))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCertificado", new { id = certificado.IdCertificado }, certificado);
        }

        // DELETE: api/Certificados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificado(int id)
        {
            if (_context.Certificados == null)
            {
                return NotFound();
            }
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return NotFound();
            }

            _context.Certificados.Remove(certificado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificadoExists(int id)
        {
            return (_context.Certificados?.Any(e => e.IdCertificado == id)).GetValueOrDefault();
        }
    }
}
