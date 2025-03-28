namespace ProgettoSettimana7.DTOs.Artist
{
    public class ArtistBaseDto
    {
        public int ArtistId { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Genre { get; set; }

        public string? Biography { get; set; }
    }
}
