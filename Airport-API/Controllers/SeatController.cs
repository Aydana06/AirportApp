using Airport.services;
using AirportLibrary.repo;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly SeatService _seatService;

        public SeatController(SeatService seatService)
        {
            _seatService = seatService;
        }

        /// <summary>
        /// Нислэгийн үлдэгдэл сул суудлуудыг буцаана.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Сул байгаа суудлуудын жагсаалт.</returns>
        [HttpGet("{flightId}")]
        public IActionResult GetAvailableSeats(int flightId)
        {
            var seats = _seatService.GetAvailableSeats(flightId);
            return Ok(seats);
        }
    }
}
