using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoSettimana7.Models.Auth
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public new required string UserId { get; set; }

        public new required string RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(RoleId))]
        public ApplicationRole ApplicationRole { get; set; }
    }
}
