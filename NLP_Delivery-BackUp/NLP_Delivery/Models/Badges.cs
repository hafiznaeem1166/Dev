using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class Badges
    {
        
            [Key]
            public int BadgeID { get; set; }

            [Required]
            [StringLength(50)]
            public string? BadgeName { get; set; }
       
        

    }
}
