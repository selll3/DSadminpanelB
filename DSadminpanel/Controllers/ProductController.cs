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
    [Route("api/products")]

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DSDbContext _context;

        public ProductController(DSDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<modified8>>> GetProducts(int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                var totalItems = await _context.modified8.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                var skipAmount = (pageNumber - 1) * pageSize;

                var products = await _context.modified8
                    .OrderBy(p => p.urunid)  // Burada sıralama işlemi yapılır (ID veya istediğiniz bir alan)
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToListAsync();

                var result = new
                {
                    totalItems,
                    totalPages,
                    pageNumber,
                    pageSize,
                    products
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return StatusCode(500, "Sunucu hatası oluştu.");
            }

        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<modified8>> GetProduct(int id)
        {
            var product = await _context.modified8.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<modified8>> PostProduct(modified8 product)
        {
            // Validasyon işlemi
            if (product == null)
            {
                return BadRequest();
            }

            _context.modified8.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.urunid }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, modified8 product)
        {
            if (id != product.urunid)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm, int pageNumber = 1, int pageSize = 50)
        {
            // 1️⃣ Arama terimi boşsa hata dön
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new { message = "Arama terimi gereklidir." });
            }

            try
            {
                var query = _context.modified8.AsQueryable();

                // 2️⃣ Filtreleme yap
                query = query.Where(p => p.İsim.Contains(searchTerm) || p.Ürün_kodu.Contains(searchTerm));

                // 3️⃣ Toplam kayıt sayısını al
                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var skipAmount = (pageNumber - 1) * pageSize;

                // 4️⃣ Sayfalama işlemi uygula
                var products = await query
                    .OrderBy(p => p.urunid) // Verileri sıralıyoruz
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToListAsync();

                // 5️⃣ Sonucu döndür
                return Ok(new
                {
                    products,
                    totalPages,
                    totalItems,
                    pageNumber
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Arama hatası: {ex.Message}");
                return StatusCode(500, new { message = "Sunucu hatası oluştu." });
            }
        }



        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.modified8.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.modified8.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.modified8.Any(e => e.urunid == id);
        }
    }
}
