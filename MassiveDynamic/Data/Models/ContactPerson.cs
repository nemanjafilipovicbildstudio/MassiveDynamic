using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.Data.Models
{
    public class ContactPerson : BaseEntity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override string Id { get; set ; }

        public string CompanyId { get; set; }

        [Required]
        [PersonalData]
        [MaxLength(ContactPersonConfig.MaxLengthFirstName)]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [MaxLength(ContactPersonConfig.MaxLengthLastName)]
        public string LastName { get; set; }

        [Required]
        [PersonalData]
        [MaxLength(ContactPersonConfig.MaxLengthEmail)]
        public string Email { get; set; }

        [Required]
        [PersonalData]
        [MaxLength(ContactPersonConfig.MaxLengthPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [PersonalData]
        [MaxLength(ContactPersonConfig.MaxLengthAddress)]
        public string Address { get; set; }

        public Company Company { get; set; }
    }
}
