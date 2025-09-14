using AirportLibrary;
using Airport.Api.Models;
using Airport.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AirportLibrary.repo;
using AirportLibrary.services;
using Airport.services;

namespace Airport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInController : ControllerBase
    {
        private readonly PassengerService _passengerService;
        private readonly SeatService _seatService;
        private readonly IHubContext<SeatHub> _hub;

        public CheckInController(PassengerService passengerService, SeatService seatService, IHubContext<SeatHub> hub)
        {
            _passengerService = passengerService;
            _seatService = seatService;
            _hub = hub;
        }

        /// <summary>
        /// Зорчигчийг бүртгэж, сонгосон суудлыг онооно.
        /// </summary>
        /// <param name="request">Паспортын дугаар, суудлын дугаар болон нислэгийн ID агуулсан хүсэлт.</param>
        /// <returns>Амжилттай бол OK, алдааны мэдээлэлтэй статус буцаана.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CheckInRequest request)
        {
            var passenger = _passengerService.GetPassengerByPassport(request.PassportNo);
            if (passenger == null)
                return NotFound("Пасспортын дугаараар зорчигч олдсонгүй");

            var alreadyAssigned = _seatService.GetSeatForPassenger(passenger.Id, request.FlightId);
            if (alreadyAssigned != null)
                return Conflict($"Зорчигчид {alreadyAssigned.SeatNo} суудал аль хэдийн оноогдсон байна");

            var seat = _seatService.GetSeatDetails(request.SeatNo, request.FlightId);
            if (seat == null)
                return NotFound("Ийм суудал олдсонгүй");

            if (seat.isTaken)
                return Conflict("Суудал аль хэдийн сонгогдсон байна");

            var success = _seatService.AssignSeat(passenger.Id, request.SeatNo, request.FlightId);
            if (!success)
                return StatusCode(500, "Суудал оноох үед алдаа гарлаа");

            await _hub.Clients.All.SendAsync("SeatTaken", request.SeatNo);

            return Ok("Суудал амжилттай оноогдлоо");
        }
    }
}