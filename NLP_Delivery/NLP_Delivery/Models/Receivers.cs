using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLP_Delivery.Models
{
    public class Receivers
    {
        [Key]
        public int ReceiverID { get; set; }

        [Required]
        [StringLength(100)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(100)]
        public string ReceiverAddress { get; set; }
       
        //[ForeignKey("RoleID")]
        //public Roles Role { get; set; }
    }
}
