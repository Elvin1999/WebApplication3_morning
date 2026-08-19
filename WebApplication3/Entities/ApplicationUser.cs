using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
