using Microsoft.AspNetCore.Mvc.RazorPages;
using AirportWebApp.Models;
using System;
using System.Collections.Generic;

namespace AirportWebApp.Pages
{
    public class DepartureModel : PageModel
    {
        public List<FlightStatus> Flights { get; set; }

        public void OnGet()
        {
            // Жишээ өгөгдөл
            Flights = new List<FlightStatus>
            {
                new FlightStatus { FlightNumber = "OM301", Destination = "Ulaanbaatar", DepartureTime = DateTime.Now.AddMinutes(45), Gate = "A12", Status = "On Time" },
                new FlightStatus { FlightNumber = "OM402", Destination = "Seoul", DepartureTime = DateTime.Now.AddHours(1), Gate = "B05", Status = "Boarding" },
                new FlightStatus { FlightNumber = "OM501", Destination = "Tokyo", DepartureTime = DateTime.Now.AddMinutes(-10), Gate = "C07", Status = "Departed" },
                new FlightStatus { FlightNumber = "OM207", Destination = "Beijing", DepartureTime = DateTime.Now.AddMinutes(30), Gate = "A04", Status = "Delayed" }
            };
        }
    }
}
