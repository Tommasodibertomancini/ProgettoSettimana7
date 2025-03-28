using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class EditTicketResponse
    {
        [Required]
        public required string Message { get; set; }
    }
}
