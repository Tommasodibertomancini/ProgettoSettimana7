using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class GetTicketResponse
    {
        [Required]
        public required string Message { get; set; }

        public TicketDto? Ticket { get; set; }
    }
}
