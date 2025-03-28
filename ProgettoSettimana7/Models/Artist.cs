using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Genre { get; set; }

        public string? Biography { get; set; }

        public ICollection<Event>? Events { get; set; }
    }
}
