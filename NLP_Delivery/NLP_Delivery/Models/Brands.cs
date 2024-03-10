using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class Brands
    {
        [Key]
        public int BrandID { get; set; }

        [Required]
        [StringLength(50)]
        public string BrandName { get; set; }
    }

}
