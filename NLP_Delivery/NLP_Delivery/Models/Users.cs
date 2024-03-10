using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NLP_Delivery.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [ForeignKey("RoleID")]
        public Roles Role { get; set; }
    }

}
