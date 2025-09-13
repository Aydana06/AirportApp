using Airport.services;
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
        /// Нислэгийн бүх суудлуудыг буцаана (боломжтой болон эзлэгдсэн).
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <returns>Бүх суудлуудын жагсаалт.</returns>
        [HttpGet("{flightId}")]
        public IActionResult GetAllSeats(int flightId)
        {
            try
            {
                var seats = _seatService.GetAllSeats(flightId);
                return Ok(seats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Алдаа гарлаа: {ex.Message}");
            }
        }
    }
}
