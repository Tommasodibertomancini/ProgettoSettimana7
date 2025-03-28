using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Event
{
    public class EditEventRequestDto
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required string Place { get; set; }

        [Required]
        public int ArtistId { get; set; }
    }
}
