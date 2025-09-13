using AirportLibrary.repo;
using AirportLibrary.services;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passengerService;
        private readonly FlightService _flightService;

        public PassengerController(PassengerService passengerService, FlightService flightService)
        {
            _passengerService = passengerService;
            _flightService = flightService;
        }

        /// <summary>
        /// Паспортын дугаараар зорчигчийн мэдээллийг авна.
        /// </summary>
        /// <param name="passportNo">Зорчигчийн паспортын дугаар.</param>
        /// <returns>Зорчигч олдвол мэдээллийг буцаана, олдохгүй бол NotFound буцаана.</returns>
        [HttpGet("{passportNo}")]
        public IActionResult GetByPassport(string passportNo)
        {
            var passenger = _passengerService.GetPassengerByPassport(passportNo);
            if (passenger == null)
                return NotFound();

            // Зорчигчийн мэдээлэлтэй хамт нислэгийн төлөвийг буцаах
            var flight = _flightService.GetFlightById(passenger.FlightId);
            var result = new
            {
                Id = passenger.Id,
                FullName = passenger.Name,
                PassportNo = passenger.PassportNo,
                FlightId = passenger.FlightId,
                SeatNo = passenger.SeatNo,
                FlightStatus = flight?.Status ?? "Тодорхойгүй"
            };

            return Ok(result);
        }
    }
}
