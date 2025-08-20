namespace Airport.Api.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string Departure { get; set; } = string.Empty;
        public string Status { get; set; } = "";
    }
}
