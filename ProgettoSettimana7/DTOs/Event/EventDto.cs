using System.ComponentModel.DataAnnotations.Schema;
using ProgettoSettimana7.DTOs.Artist;

namespace ProgettoSettimana7.DTOs.Event
{
    public class EventDto
    {
        public int Eventid { get; set; }

        public required string Title { get; set; }

        public DateTime Date { get; set; }

        public required string Place { get; set; }

        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public ArtistBaseDto Artist { get; set; }
    }
}
