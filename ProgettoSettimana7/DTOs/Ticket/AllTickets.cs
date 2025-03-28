using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class AllTickets
    {
        [Required]
        public required string Message { get; set; }

        public List<TicketDto>? Tickets { get; set; }
    }
}
