using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProgettoSettimana7.Models.Auth;
using ProgettoSettimana7.DTOs.Account;
using ProgettoSettimana7.DTOs.Event;

namespace ProgettoSettimana7.DTOs.Ticket
{
    public class TicketDto
    {
        [Required]
        public int Ticketid { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public required string UserId { get; set; }

        [ForeignKey(nameof(EventId))]
        public BaseEventDto Event { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUserDto ApplicationUser { get; set; }

    }
}
