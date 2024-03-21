using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace NLP_Delivery.Models
{
    public class AppUserViewModel
    {
        public int UserID { get; set; }

        public string?  UserName { get; set; }


        public string Password { get; set; }

        
        public int RoleID { get; set; }
        
        
        public string? PhoneNumber { get; set; }

        
        public string? Email { get; set; }

        public List<IdentityRole<int>>? AppRoles { get; set; }      
    }

}
