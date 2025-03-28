using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Account
{
    public class ApplicationUserDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
