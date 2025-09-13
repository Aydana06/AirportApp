using AirportLibrary.repo;
using AirportLibrary.model;
using Airport.services;

namespace AirportLibrary.services
{
    public class FlightService
    {
        private readonly IFlightRepository _repository;

        public FlightService(IFlightRepository repository)
        {
            _repository = repository;
        }

        public void UpdateFlightStatus(int flightId, string newStatus)
        {
            var flight = _repository.GetById(flightId);
            if (flight == null)
            {
                throw new Exception("Flight not found");
            }

            flight.Status = newStatus;
            _repository.Update(flight);

        }

        public List<Flight> ListAllFlights()
        {
            return _repository.GetAll();
        }

        public Flight? GetFlightById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            return _repository.GetById(id);
        }

        /// <summary>
        /// FlightCode-оор нислэг хайх сервис
        /// </summary>
        public Flight? GetFlightByCode(string flightCode)
        {
            if (string.IsNullOrWhiteSpace(flightCode))
                throw new ArgumentException("Flight code хоосон байж болохгүй.");

            var flight = _repository.GetByCode(flightCode);

            if (flight == null)
            {
                // энд алдаа хаяж болно эсвэл null буцааж болно
                Console.WriteLine($"Flight {flightCode} олдсонгүй.");
                return null;
            }

            return flight;
        }
    }

}
