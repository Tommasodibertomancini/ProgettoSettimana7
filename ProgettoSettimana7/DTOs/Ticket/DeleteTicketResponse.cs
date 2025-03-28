using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class DeleteTicketResponse
    {
        [Required]
        public required string Message { get; set; }

    }
}
