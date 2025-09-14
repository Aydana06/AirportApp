using Airport.Api.Hubs;
using AirportLibrary;
using AirportLibrary.repo;
using AirportLibrary.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Airport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;
        private readonly IHubContext<FlightHub> _hub;

        public FlightController(FlightService flightService, IHubContext<FlightHub> hub)
        {
            _flightService = flightService;
            _hub = hub;
        }

        /// <summary>
        /// Бүх нислэгийн мэдээллийг буцаана.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllFlights()
        {
            var flights = _flightService.ListAllFlights();
            return Ok(flights);
        }

        /// <summary>
        /// Нислэгийн ID-р тодорхой нислэгийн мэдээллийг буцаана.
        /// </summary>
        /// <param name="id">Нислэгийн ID</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flight = _flightService.GetFlightById(id);
            if (flight == null)
                return NotFound("Нислэг олдсонгүй");

            return Ok(flight);
        }

        /// <summary>
        /// Нислэгийн төлөв (Status)-ийг шинэчилнэ.
        /// </summary>
        /// <param name="dto">Нислэгийн ID болон шинэ төлөв агуулсан DTO.</param>
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] FlightStatusDto dto)
        {
            var flight = _flightService.GetFlightByCode(dto.FlightCode);
            if (flight == null)
                return NotFound("Нислэг олдсонгүй");

            flight.Status = dto.Status;
            _flightService.UpdateFlightStatus(flight.Id, flight.Status);

            await _hub.Clients.All.SendAsync("FlightStatusUpdated", new
            {
                FlightId = flight.Id,
                flight.FlightCode,
                status = flight.Status
            });

            return Ok("Нислэгийн төлөв амжилттай солигдлоо.");
        }

        /// <summary>
        /// Нислэгийн төлөв шинэчлэхэд ашиглагдах DTO.
        /// </summary>
        public class FlightStatusDto
        {
            public string FlightCode { get; set; } = "";
            public string Status { get; set; } = "";
        }

    }
}