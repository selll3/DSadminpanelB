using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DSadminpanel.Data;
using DSadminpanel.Services;
using DSadminpanel.Models;
using Microsoft.OpenApi.Models;  // Swagger için gerekli namespace

var builder = WebApplication.CreateBuilder(args);

// JwtSettings'i yapýlandýrma
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Veritabaný baðlantýsýný yapýlandýrma
builder.Services.AddDbContext<DSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JwtService'in DI konteynerine kaydedilmesi
builder.Services.AddScoped<JwtService>();

// CORS ayarlarýný yapýlandýrma
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Ýsteklerin her kaynaktan gelmesine izin verir
              .AllowAnyMethod()  // Tüm HTTP yöntemlerini (GET, POST, vb.) kabul eder
              .AllowAnyHeader(); // Tüm header'lara izin verir
    });
});

// JWT doðrulama ayarlarýný yapýlandýrma
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });


builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

// Swagger yapýlandýrmasý
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DS Admin Panel API",
        Version = "v1"
    });
});

//.WebHost.UseUrls//(//"http:////localhost:5285");



var app = builder.Build();

// Swagger middleware'ini ekleyin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Swagger JSON dökümantasyonunu saðlar
    app.UseSwaggerUI(c =>  // Swagger UI'yi baþlatýr
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DS Admin Panel API V1");
        c.RoutePrefix = string.Empty;  // Swagger UI'yi kök URL'ye yerleþtirir
    });
}

// Middleware'i ayarla
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
