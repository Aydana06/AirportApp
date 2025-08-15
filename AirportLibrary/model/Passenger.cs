using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLibrary.model
{
    public class Passenger
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string PassportNo { get; set; }
        public int FlightId { get; set; } 
        public string? SeatNo { get; set; }
    }

}
