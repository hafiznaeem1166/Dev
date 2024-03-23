using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NLP_Delivery.Models
{
    public class UniformDispensing
    {
        [Key]
        public int DispensingID { get; set; }

        [Required]
        public int StoreID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int ReceiverID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int NumProducts { get; set; }

        public byte[] SignatureImage { get; set; }

        [ForeignKey("StoreID")]
        public Stores Store { get; set; }

        [ForeignKey("UserID")]
        public IdentityUser<int> User { get; set; }

        [ForeignKey("ReceiverID")]
        public Receivers Receiver { get; set; }

        [ForeignKey("ProductID")]
        public Products Product { get; set; }

    }
}
