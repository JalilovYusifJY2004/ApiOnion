using Microsoft.AspNetCore.Identity;

namespace ApiOnion104.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }

    }
}
