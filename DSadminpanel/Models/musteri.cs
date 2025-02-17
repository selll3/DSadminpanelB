using System.ComponentModel.DataAnnotations;

namespace DSadminpanel.Models
{
    public class musteri
    {
        [Key]
        public int musteri_id { get; set; }

        [Required]
        [StringLength(255)]
        public string ad_soyad_firma { get; set; }

        [Required]
        [StringLength(20)]
        public string telefon { get; set; }

        [Required]
        public string adres { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        public virtual List<teklif> teklif { get; set; } = new();
    }
}
