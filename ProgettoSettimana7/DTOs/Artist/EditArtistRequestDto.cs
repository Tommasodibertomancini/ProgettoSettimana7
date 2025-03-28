using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Artist
{
    public class EditArtistRequestDto
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Genre { get; set; }

        public string? Biography { get; set; }
    }
}
