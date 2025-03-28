using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Event
{
    public class EditEventResponse
    {
        [Required]
        public required string Message { get; set; }
    }
}
