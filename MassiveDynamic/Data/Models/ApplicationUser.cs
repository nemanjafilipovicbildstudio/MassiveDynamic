using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MassiveDynamic.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [MaxLength(128)]
        public string LastName { get; set; }
    }
}
