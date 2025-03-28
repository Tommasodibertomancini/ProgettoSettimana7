using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Artist
{
    public class CreateArtistResponse
    {
        [Required]
        public required string Message { get; set; }
    }
}
