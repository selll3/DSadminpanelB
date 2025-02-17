using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSadminpanel.Models
{
    public class teklif_urunleri
    {
        public int id { get; set; }

        public int teklif_id { get; set; }
        [ForeignKey("id")]
        public virtual teklif teklif { get; set; }

        public int urun_id { get; set; }

        [ForeignKey("urunid")]
        public virtual modified8 Modified8 { get; set; }

        public int miktar { get; set; } = 1;

        public decimal? birim_fiyat { get; set; } = 0;

        public decimal? tutar { get; set; } = 0;

        public decimal? iskonto { get; set; } = 0;

        [StringLength(50)]
        public string teslim_suresi { get; set; }
    }
}
