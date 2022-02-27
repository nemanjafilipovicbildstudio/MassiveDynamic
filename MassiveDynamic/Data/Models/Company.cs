using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MassiveDynamic.Data.ModelConfigs;

namespace MassiveDynamic.Data.Models
{
    public class Company : BaseEntity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [MaxLength(CompanyModelConfig.MaxLengthId)]
        override public string Id { get; set; }

        [Required]
        [MaxLength(CompanyModelConfig.MaxLengthName)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CompanyModelConfig.MaxLengthAddress)]
        public string Address { get; set; }

        public ICollection<ContactPerson> ContactPersons { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
