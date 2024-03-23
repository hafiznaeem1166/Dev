using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class ProductType
    {

        [Key]
        public int ProductTypeID { get; set; }
    
        [Required]
        [StringLength(100)]
        public string ProductTypeName { get; set; }

    }
}
