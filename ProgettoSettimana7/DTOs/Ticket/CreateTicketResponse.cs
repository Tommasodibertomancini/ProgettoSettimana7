using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class CreateTicketResponse
    {
        [Required]
        public required string Message { get; set; }

    }
}
