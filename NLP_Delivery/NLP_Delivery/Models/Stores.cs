using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NLP_Delivery.Models
{
    public class Stores
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        public int BrandID { get; set; }

        [Required]
        public int AddressID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreName { get; set; }

        [ForeignKey("BrandID")]
        public Brands Brand { get; set; }

        [ForeignKey("AddressID")]
        public Addresses Address { get; set; }
    }

}
