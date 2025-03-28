using ProgettoSettimana7.Models.Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProgettoSettimana7.Models
{
    public class Ticket
    {
        [Key]
        public int Ticketid { get; set; }

        [Required]
        public DateTime DateBought { get; set; }

        public int ArtistId { get; set; }

        public int EventId { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
