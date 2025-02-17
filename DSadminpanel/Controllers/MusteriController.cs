namespace DSadminpanel.Controllers
{
 
    using DSadminpanel.Data;
    using DSadminpanel.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/musteri")]
    [ApiController]
    public class MusteriController : ControllerBase
    {
        private readonly DSDbContext _context;

        public MusteriController(DSDbContext context)
        {
            _context = context;
        }

        // 📌 Tüm müşterileri listele
        [HttpGet]
        public async Task<ActionResult<IEnumerable<musteri>>> GetMusteriler()
        {
            return await _context.musteri.ToListAsync();
        }

        // 📌 ID'ye göre müşteri getir
        [HttpGet("{id}")]
        public async Task<ActionResult<musteri>> GetMusteri(int id)
        {
            var musteri = await _context.musteri.FindAsync(id);

            if (musteri == null)
            {
                return NotFound();
            }

            return musteri;
        }

        // 📌 Yeni müşteri ekle
        [HttpPost]
        public async Task<ActionResult<musteri>> PostMusteri(musteri musteri)
        {
            _context.musteri.Add(musteri);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMusteri), new { id = musteri.musteri_id }, musteri);
        }

        // 📌 Müşteri güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusteri(int id, musteri musteri)
        {
            if (id != musteri.musteri_id)
            {
                return BadRequest();
            }

            _context.Entry(musteri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.musteri.Any(e => e.musteri_id == id))
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

        // 📌 Müşteri sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusteri(int id)
        {
            var musteri = await _context.musteri.FindAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }

            _context.musteri.Remove(musteri);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
