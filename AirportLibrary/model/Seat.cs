using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLibrary.model
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNo { get; set; }  
        public int FlightId { get; set; }
        public bool isTaken { get; set; }
    }
}
