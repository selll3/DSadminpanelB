using Microsoft.AspNetCore.Mvc;
using DSadminpanel.Models;
using DSadminpanel.Services;
using DSadminpanel.Data; // Veritabanı bağlamı için eklendi
using Microsoft.Extensions.Logging; // Loglama için

namespace DSadminpanel.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly DSDbContext _dbContext; // Veritabanı bağlamı
        private readonly ILogger<AdminController> _logger; // Loglama servisi

        public AdminController(JwtService jwtService, DSDbContext dbContext, ILogger<AdminController> logger)
        {
            _jwtService = jwtService;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Gelen isteği kontrol et
            if (loginRequest == null)
            {
                _logger.LogError("Login request is null.");
                return BadRequest(new { Message = "Invalid request format." });
            }

            _logger.LogInformation($"Login attempt by username: {loginRequest.Username}");

            // Veritabanında kullanıcı doğrulama
            var admin = _dbContext.Admins
                .FirstOrDefault(a => a.Username == loginRequest.Username && a.Password == loginRequest.Password);

            if (admin != null)
            {
                var token = _jwtService.GenerateToken(admin.Username);
                _logger.LogInformation($"Login successful for username: {admin.Username}");

                return Ok(new LoginResponse
                {
                    Message = "Login Successful",
                    Token = token
                });
            }

            // Hatalı giriş
            _logger.LogWarning($"Invalid login attempt for username: {loginRequest.Username}");
            return Unauthorized(new { Message = "Invalid username or password." });
        }
    }
}