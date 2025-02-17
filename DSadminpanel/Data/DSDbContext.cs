using Microsoft.EntityFrameworkCore;
using DSadminpanel.Models;
using DSadminpanel.Migrations;



namespace DSadminpanel.Data
{
    public class DSDbContext : DbContext
    {
        public DSDbContext(DbContextOptions<DSDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<musteri> musteri { get; set; } 

        public  DbSet<modified8> modified8 { get; set; }
        public DbSet<teklif> teklif { get; set; }

        public DbSet<teklif_urunleri> teklif_urunleri { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Decimal alanlar için veri tipi belirtme
            modelBuilder.Entity<teklif>()
                .Property(t => t.genel_toplam)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif>()
                .Property(t => t.kdv)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif>()
                .Property(t => t.toplam)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif>()
                .Property(t => t.toplam_euro)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif>()
                .Property(t => t.toplam_tl)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif>()
                .Property(t => t.toplam_usd)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif_urunleri>()
                .Property(t => t.birim_fiyat)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif_urunleri>()
                .Property(t => t.iskonto)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<teklif_urunleri>()
                .Property(t => t.tutar)
                .HasColumnType("decimal(18, 2)");

            // Diðer model konfigürasyonlarýný burada yapabilirsiniz
        }

    }




}
