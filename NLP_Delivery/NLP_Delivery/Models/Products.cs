using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace NLP_Delivery.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int BrandID { get; set; }

        [Required]
        public int SizeID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [ForeignKey("BrandID")]
        public Brands Brand { get; set; }

        [ForeignKey("SizeID")]
        public Sizes Size { get; set; }
    }
}
