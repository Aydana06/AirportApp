using System;

namespace AirportWebApp.Models
{
    public class FlightStatus
    {
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Gate { get; set; }
        public string Status { get; set; }
    }
}
