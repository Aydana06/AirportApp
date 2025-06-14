using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLibrary.repo;
using AirportLibrary.model;

namespace AirportLibrary.services
{
    public class FlightService
    {
        private readonly FlightRepository _repository;

        public FlightService(FlightRepository repository)
        {
            _repository = repository;
        }

        public void UpdateFlightStatus(int flightId, string newStatus)
        {
            var flight = _repository.GetById(flightId);
            if (flight == null) {
                throw new Exception("Flight not found");
            }

            flight.Status = newStatus;
            _repository.Update(flight);

        }

        public List<Flight> ListAllFlights()
        {
            return _repository.GetAll();
        }
    }

}
