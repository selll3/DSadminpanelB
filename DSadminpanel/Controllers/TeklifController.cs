using DSadminpanel.Models;
using DSadminpanel.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSadminpanel.Controllers
{
    [Route("api/teklif")]
    [ApiController]
    public class TeklifController : ControllerBase
    {
        private readonly DSDbContext _context;

        public TeklifController(DSDbContext context)
        {
            _context = context;
        }

        // Tüm teklifleri getir
        [HttpGet]
        public async Task<ActionResult<IEnumerable<teklif>>> GetTeklifler()
        {
            return await _context.teklif
                .Include(t => t.musteri)
                .Include(t => t.teklif_urunleri)
                .ToListAsync();
        }

        // Belirli bir teklifi getir
        [HttpGet("{id}")]
        public async Task<ActionResult<teklif>> GetTeklif(int id)
        {
            var teklif = await _context.teklif
                .Include(t => t.musteri)
                .Include(t => t.teklif_urunleri)
                .FirstOrDefaultAsync(t => t.id == id);

            if (teklif == null)
            {
                return NotFound();
            }

            return teklif;
        }

        // Yeni teklif oluştur
        [HttpPost]
        public async Task<ActionResult<teklif>> CreateTeklif(teklif yeniTeklif)
        {
            _context.teklif.Add(yeniTeklif);

            // Eğer teklif içinde ürünler varsa onları da kaydet
            if (yeniTeklif.teklif_urunleri != null && yeniTeklif.teklif_urunleri.Any())
            {
                foreach (var urun in yeniTeklif.teklif_urunleri)
                {
                    _context.teklif_urunleri.Add(urun);
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeklif), new { id = yeniTeklif.id }, yeniTeklif);
        }


        // Teklif güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeklif(int id, teklif guncellenmisTeklif)
        {
            if (id != guncellenmisTeklif.id)
            {
                return BadRequest();
            }

            _context.Entry(guncellenmisTeklif).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeklifVarMi(id))
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

        // Teklif sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeklif(int id)
        {
            var teklif = await _context.teklif.FindAsync(id);
            if (teklif == null)
            {
                return NotFound();
            }

            _context.teklif.Remove(teklif);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeklifVarMi(int id)
        {
            return _context.teklif.Any(e => e.id == id);
        }
    }

}
