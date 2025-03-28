using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Event
{
    public class AllEvent
    {
        [Required]
        public required string Message { get; set; }

        public List<EventDto>? Events { get; set; }
    }
}
