using System.ComponentModel.DataAnnotations.Schema;

namespace DSadminpanel.Models
{
    public class teklif
    {
        public int id { get; set; }

        public int musteriid { get; set; }

        [ForeignKey("Musteriid")]
        public virtual musteri musteri { get; set; }

        [Column(TypeName = "date")]
        public DateTime teklif_tarihi { get; set; }

        [Column(TypeName = "date")]
        public DateTime gecerlilik_tarihi { get; set; }

        public decimal? toplam_tl { get; set; } = 0;

        public decimal? toplam_usd { get; set; } = 0;

        public decimal? toplam_euro { get; set; } = 0;

        public decimal? toplam { get; set; } = 0;

        public decimal? kdv { get; set; } = 0;

        public decimal? genel_toplam { get; set; } = 0;

        public virtual List<teklif_urunleri> teklif_urunleri { get; set; } = new();
    }
}
