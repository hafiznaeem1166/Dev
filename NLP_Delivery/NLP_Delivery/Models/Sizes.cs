using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class Sizes
    {
        [Key]
        public int SizeID { get; set; }

        [Required]
        [StringLength(50)]
        public string SizeName { get; set; }
    }
}
