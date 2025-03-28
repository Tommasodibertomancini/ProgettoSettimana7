using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }

    }
}
