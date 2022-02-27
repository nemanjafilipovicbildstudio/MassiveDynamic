using System.Collections.Generic;

namespace MassiveDynamic.DTOs
{
    public class GetApplicationUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
