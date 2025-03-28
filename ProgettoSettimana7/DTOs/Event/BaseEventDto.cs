namespace ProgettoSettimana7.DTOs.Event
{
    public class BaseEventDto
    {
        public int Eventid { get; set; }

        public required string Title { get; set; }

        public DateTime Date { get; set; }

        public required string Place { get; set; }
    }
}
