using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace ProgettoSettimana7.Models
{
    public class Event
    {
        [Key]
        public int Eventid { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required string Place { get; set; }

        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }

        public ICollection<Ticket>? Tickets { get; set; }
    }
}
