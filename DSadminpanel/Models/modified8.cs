using System.ComponentModel.DataAnnotations;

namespace DSadminpanel.Models
{
    public class modified8
    {
        [Required]
        [StringLength(150)]
        public string Ürün_kodu { get; set; }

        [StringLength(350)]
        public string? İsim { get; set; }

        [StringLength(50)]
        public string? DVZ { get; set; }

        public decimal? Birim_fiyat { get; set; }

        public decimal? İSK { get; set; }

        [StringLength(50)]
        public string? Marka { get; set; }

        [StringLength(50)]
        public string? TESLİM { get; set; }

        [StringLength(50)]
        public string? MİKTAR { get; set; }

        [StringLength(50)]
        public string? Birim { get; set; }

        [StringLength(250)]
        public string? TEMİN { get; set; }

        public double? Kutu_Miktarı { get; set; }

        [StringLength(255)]
        public string? Etiketler { get; set; }

        public double? KDV { get; set; }

        public int? Stok { get; set; }

        [StringLength(255)]
        public string? Ana_kategori { get; set; }

        public bool? Stok_kullanır { get; set; }

        public int? Kritik_stok_miktarı { get; set; }

        [Key]
        public int urunid { get; set; }

        public virtual List<teklif_urunleri> teklif_urunleri { get; set; } = new();
    }
}
