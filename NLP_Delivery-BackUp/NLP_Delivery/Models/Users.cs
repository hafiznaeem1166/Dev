using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NLP_Delivery.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }
       
        [Required]
        [StringLength(50)]
        public string EmailID { get; set; }


        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [ForeignKey("RoleID")]
        public Roles Role { get; set; }
    }
    public class AppUserViewModel
    {
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
   
        public string EmailID { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }

}
