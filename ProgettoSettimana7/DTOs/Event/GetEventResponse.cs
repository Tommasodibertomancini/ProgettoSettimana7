using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Event
{
    public class GetEventResponse
    {
        [Required]
        public required string Message { get; set; }

        public EventDto? Event { get; set; }
    }
}
