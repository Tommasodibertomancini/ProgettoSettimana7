namespace ProgettoSettimana7.DTOs.Account
{
    public class TokenResponseDto
    {
        public required string Token { get; set; }

        public required DateTime Expires { get; set; }
    }
}
