using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Event
{
    public class CreateEventResponse
    {
        [Required]
        public required string Message { get; set; }
    }
}
