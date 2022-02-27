using System.Collections.Generic;

namespace MassiveDynamic.DTOs
{
    public class GetCompanyDto : CompanyDto
    {
        public IEnumerable<ContactPersonDto> ContactPersons { get; set; }
    }
}
