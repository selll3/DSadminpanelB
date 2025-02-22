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
    [Route("api/teklifurunleri")]
    [ApiController]
    public class TeklifUrunController : ControllerBase
    {
        private readonly DSDbContext _context;

        public TeklifUrunController(DSDbContext context)
        {
            _context = context;
        }

        // Bir teklifin tüm ürünlerini getir
        [HttpGet("teklif/{teklifId}")]
        public async Task<ActionResult<IEnumerable<teklif_urunleri>>> GetTeklifUrunleri(int teklifId)
        {
            return await _context.teklif_urunleri
                .Where(tu => tu.teklif_id == teklifId)
                .ToListAsync();
        }

        // Belirli bir teklif_urunünü getir
        [HttpGet("{id}")]
        public async Task<ActionResult<teklif_urunleri>> GetTeklifUrun(int id)
        {
            var teklifUrun = await _context.teklif_urunleri.FindAsync(id);

            if (teklifUrun == null)
            {
                return NotFound();
            }

            return teklifUrun;
        }

        // Yeni teklif_urunü ekle
        [HttpPost]
        public async Task<ActionResult<teklif_urunleri>> CreateTeklifUrun(teklif_urunleri yeniUrun)
        {
            _context.teklif_urunleri.Add(yeniUrun);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeklifUrun), new { id = yeniUrun.id }, yeniUrun);
        }

        // Teklif_urunü güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeklifUrun(int id, teklif_urunleri guncellenmisUrun)
        {
            if (id != guncellenmisUrun.id)
            {
                return BadRequest();
            }

            _context.Entry(guncellenmisUrun).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeklifUrunVarMi(id))
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

        // Teklif_urunü sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeklifUrun(int id)
        {
            var teklifUrun = await _context.teklif_urunleri.FindAsync(id);
            if (teklifUrun == null)
            {
                return NotFound();
            }

            _context.teklif_urunleri.Remove(teklifUrun);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeklifUrunVarMi(int id)
        {
            return _context.teklif_urunleri.Any(e => e.id == id);
        }
    }
}
