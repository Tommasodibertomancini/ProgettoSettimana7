using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Artist
{
    public class GetArtistResponse
    {
        [Required]
        public required string Message { get; set; }

        public ArtistDto? Artist { get; set; }
    }
}
