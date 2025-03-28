using System.ComponentModel.DataAnnotations;
using ProgettoSettimana7.DTOs.Event;

namespace ProgettoSettimana7.DTOs.Artist
{
    public class ArtistDto
    {
        [Required]
        public int ArtistId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Genre { get; set; }

        public string? Biography { get; set; }

        public ICollection<BaseEventDto>? Events { get; set; }
    }
}
