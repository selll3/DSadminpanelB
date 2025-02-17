//using BCrypt.Net;
//using DSadminpanel.Data;
//using Microsoft.EntityFrameworkCore;

//public class AdminService
//{
//    private readonly DSDbContext _context;

//    public AdminService(DSDbContext context)
//    {
//        _context = context;
//    }

//    // Admin şifrelerini yeniden hash'le
//    public async Task RehashPasswordsAsync()
//    {
//        var admins = await _context.Admins.ToListAsync(); // Adminleri al

//        foreach (var admin in admins)
//        {
//            // Eski şifreyi hash'le
//            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.Password);
//            admin.Password = newHashedPassword; // Yeni hash'li şifreyi ata
//        }

//        await _context.SaveChangesAsync(); // Değişiklikleri kaydet
//    }
//}
