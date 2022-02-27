using System.ComponentModel.DataAnnotations;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.DTOs
{
    public class ContactPersonDto
    {
        public string Id { get; set; }

        [Required]
        public string CompanyId { get; set; }

        [Required]
        [MaxLength(ContactPersonConfig.MaxLengthFirstName)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ContactPersonConfig.MaxLengthLastName)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(ContactPersonConfig.MaxLengthEmail)]
        public string Email { get; set; }

        [Required]
        [MaxLength(ContactPersonConfig.MaxLengthPhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(ContactPersonConfig.MaxLengthAddress)]
        public string Address { get; set; }
    }
}
