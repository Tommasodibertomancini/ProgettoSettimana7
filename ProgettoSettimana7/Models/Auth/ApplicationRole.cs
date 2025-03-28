using Microsoft.AspNetCore.Identity;

namespace ProgettoSettimana7.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
