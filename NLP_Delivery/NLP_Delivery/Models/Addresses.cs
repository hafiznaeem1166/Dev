using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class Addresses
    {
        [Key]
        public int AddressID { get; set; }

        [Required]
        [StringLength(100)]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        public string ZipCode { get; set; }
    }

}
