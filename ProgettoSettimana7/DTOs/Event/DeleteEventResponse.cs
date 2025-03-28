using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Event
{
    public class DeleteEventResponse
    {
        [Required]
        public required string Message { get; set; }
    }
}
