using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
