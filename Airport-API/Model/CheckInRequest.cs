namespace Airport.Api.Models
{
    public class CheckInRequest
    {
        public string PassportNo { get; set; } = "";
        public string SeatNo { get; set; } = "";
        public int FlightId { get; set; }
    }
}
