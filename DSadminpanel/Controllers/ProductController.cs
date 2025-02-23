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
        [HttpGet("urun")]
        public async Task<IActionResult> GetUrunler([FromQuery] string search, int page = 1, int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search) || search.Length < 4)
                {
                    return Ok(new { message = "En az 4 karakter girin.", products = new List<modified8>(), totalItems = 0, totalPages = 0 });
                }

                var query = _context.modified8
                    .Where(p => p.İsim.Contains(search) || p.Ürün_kodu.Contains(search)); // Arama filtresi

                int totalItems = await query.CountAsync(); // Toplam ürün sayısı
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                int skipAmount = (page - 1) * pageSize;

                var products = await query
                    .OrderBy(p => p.urunid) // ID'ye göre sırala
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new
                {
                    products,      // Ürünler
                    totalItems,    // Toplam ürün sayısı
                    totalPages,    // Toplam sayfa sayısı
                    page           // Mevcut sayfa
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ürün arama hatası: {ex.Message}");
                return StatusCode(500, new { message = "Sunucu hatası oluştu." });
            }
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
                return BadRequest("Ürün bilgisi boş olamaz.");
            }

            _context.modified8.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.urunid }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, modified8 product)
        {
            Console.WriteLine($"Gelen Ürün ID: {id}");
            Console.WriteLine($"Gelen Ürün Adı: {product.İsim}");
            Console.WriteLine($"Gelen Ürün Kodu: {product.Ürün_kodu}");

            if (id != product.urunid)
            {
                return BadRequest("Ürün ID'si eşleşmiyor.");
            }

            var existingProduct = await _context.modified8
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.urunid == id);

            if (existingProduct == null)
            {
                return NotFound("Güncellenmek istenen ürün bulunamadı.");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                var updatedRows = await _context.SaveChangesAsync();
                if (updatedRows == 0)
                {
                    return BadRequest("Ürün güncellenmedi, lütfen kontrol edin.");
                }
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

            return Ok(product);
        }

        //[HttpGet("search")]
        //public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm, int pageNumber = 1, int pageSize = 50)
        //{
        //    // 1️⃣ Arama terimi boşsa hata dön
        //    if (string.IsNullOrWhiteSpace(searchTerm))
        //    {
        //        return BadRequest(new { message = "Arama terimi gereklidir." });
        //    }

        //    try
        //    {
        //        var query = _context.modified8.AsQueryable();

        //        // 2️⃣ Filtreleme yap (büyük/küçük harf duyarsız arama)
        //        query = query.Where(p => EF.Functions.Like(p.İsim.ToLower(), "%" + searchTerm.ToLower() + "%") ||
        //                                 EF.Functions.Like(p.Ürün_kodu.ToLower(), "%" + searchTerm.ToLower() + "%"));

        //        // 3️⃣ Toplam kayıt sayısını al
        //        var totalItems = await query.CountAsync();
        //        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        //        var skipAmount = (pageNumber - 1) * pageSize;

        //        // 4️⃣ Sayfalama işlemi uygula
        //        var products = await query
        //            .OrderBy(p => p.urunid) // Verileri sıralıyoruz
        //            .Skip(skipAmount)
        //            .Take(pageSize)
        //            .ToListAsync();

        //        // 5️⃣ Sonucu döndür
        //        return Ok(new
        //        {
        //            products,
        //            totalPages,
        //            totalItems,
        //            pageNumber
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Arama hatası: {ex.Message}");
        //        return StatusCode(500, new { message = "Sunucu hatası oluştu." });
        //    }
        //}

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm, int pageNumber = 1, int pageSize = 50)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new { message = "Arama terimi gereklidir." });
            }

            try
            {
                var query = _context.modified8.AsQueryable();

                // 2️⃣ Arama filtresi
                query = query.Where(p => p.İsim.Contains(searchTerm) || p.Ürün_kodu.Contains(searchTerm));

                // 3️⃣ Sayfalama hesaplama
                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                var skipAmount = (pageNumber > 1) ? (pageNumber - 1) * pageSize : 0;

                // 4️⃣ Sıralama & Sayfalama
                var products = await query
                    .OrderByDescending(p => p.İsim == searchTerm)  // "den" isimli ürünleri en üste al
                    .ThenByDescending(p => p.Ürün_kodu == searchTerm) // Ürün kodu da tam eşleşiyorsa üstte olsun
                    .ThenBy(p => p.urunid) // Son olarak ID sırasına göre düzenle
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToListAsync();

                // 5️⃣ Sonuç
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
