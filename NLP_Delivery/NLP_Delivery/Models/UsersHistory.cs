using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NLP_Delivery.Models
{
    public class UsersHistory
    {
        [Key]
        public int HistoryID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LogInTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LogOutTime { get; set; }

        [ForeignKey("UserID")]
        public Users User { get; set; }
    }

}
