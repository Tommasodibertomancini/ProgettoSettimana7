using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Artist
{
    public class EditArtistResponse
    {
        [Required]
        public required string Message { get; set; }
    }
}
