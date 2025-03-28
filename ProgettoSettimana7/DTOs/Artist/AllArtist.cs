using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Artist
{
    public class AllArtist
    {
        [Required]
        public required string Message { get; set; }

        public List<ArtistDto>? Artists { get; set; }
    }
}
