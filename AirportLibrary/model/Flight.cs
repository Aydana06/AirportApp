using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLibrary.model
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightCode { get; set; }
        public string Status { get; set; }
        public int TotalSeats { get; set; } = 30; // Анхны утга: 30 суудал
        public int AvailableSeats { get; set; } = 30; // Боломжтой суудлын тоо
    }
}
