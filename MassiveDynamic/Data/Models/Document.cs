using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassiveDynamic.Data.Models
{
    public class Document : BaseEntity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        override public string Id { get; set; }

        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        public DateTime UploadTime { get; set; }

        public Company Company { get; set; }
    }
}
