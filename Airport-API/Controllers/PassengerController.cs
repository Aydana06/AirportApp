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

        public PassengerController(PassengerService passengerService)
        {
            _passengerService = passengerService;
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

            return Ok(passenger);
        }
    }
}
