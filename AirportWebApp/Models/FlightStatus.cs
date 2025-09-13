using System;

namespace AirportWebApp.Models
{
    public class FlightStatus
    {
        public int Id { get; set; }
        public string FlightCode { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
