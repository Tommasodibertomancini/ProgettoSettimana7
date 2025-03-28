using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class EdiTicketRequestDto
    {
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}
