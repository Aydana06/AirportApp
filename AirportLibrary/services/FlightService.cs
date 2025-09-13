using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLibrary.repo;
using AirportLibrary.model;
using Airport.services;

namespace AirportLibrary.services
{
    public class FlightService
    {
        private readonly IFlightRepository _repository;
        private readonly SeatService _seatService;

        public FlightService(IFlightRepository repository, SeatService seatService)
        {
            _repository = repository;
            _seatService = seatService;
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

        /// <summary>
        /// Шинэ нислэг үүсгэх (суудлуудтай хамт)
        /// </summary>
        public Flight CreateFlight(string flightCode, string status, int totalSeats = 30)
        {
            if (string.IsNullOrWhiteSpace(flightCode))
                throw new ArgumentException("Flight code хоосон байж болохгүй.");

            if (totalSeats <= 0)
                throw new ArgumentException("Суудлын тоо 0-ээс их байх ёстой.");

            var flight = new Flight
            {
                FlightCode = flightCode,
                Status = status,
                TotalSeats = totalSeats,
                AvailableSeats = totalSeats
            };

            // Нислэгийг database-д хадгалах
            _repository.Create(flight);

            // Суудлуудыг автоматаар үүсгэх
            CreateSeatsForFlight(flight.Id, totalSeats);

            return flight;
        }

        /// <summary>
        /// Нислэгт суудлуудыг автоматаар үүсгэх
        /// </summary>
        private void CreateSeatsForFlight(int flightId, int totalSeats)
        {
            _seatService.CreateSeatsForFlight(flightId, totalSeats);
        }
    }

}
