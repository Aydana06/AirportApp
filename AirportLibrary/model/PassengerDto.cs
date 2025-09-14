using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLibrary.model
{
    public class PassengerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string PassportNo { get; set; } = "";
        public int FlightId { get; set; }
        public string? SeatNo { get; set; }
        public string FlightStatus { get; set; } = "";
    }
}
