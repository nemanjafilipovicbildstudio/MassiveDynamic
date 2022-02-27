using System.ComponentModel.DataAnnotations;

namespace MassiveDynamic.DTOs
{
    public class CompanyDto
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
