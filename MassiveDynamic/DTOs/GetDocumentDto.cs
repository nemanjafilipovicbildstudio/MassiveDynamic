using System;

namespace MassiveDynamic.DTOs
{
    public class GetDocumentDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime UploadTime { get; set; }
    }
}
