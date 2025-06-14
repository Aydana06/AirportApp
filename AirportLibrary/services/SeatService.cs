
using AirportLibrary.model;
using AirportLibrary.repo;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.services
{
    /// <summary>
    /// Provides high-level business logic for managing seat assignments and queries.
    /// </summary>
    public class SeatService
    {
        private readonly SeatRepository _seatRepository;

        public SeatService(SeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        /// <summary>
        /// Retrieves all available seats for a given flight.
        /// </summary>
        public List<Seat> GetAvailableSeats(int flightId)
        {
            return _seatRepository.GetAvailableSeats(flightId);
        }

        /// <summary>
        /// Checks if a specific seat is already taken on a flight.
        /// </summary>
        public bool IsSeatAlreadyTaken(int flightId, string seatNo)
        {
            return _seatRepository.IsSeatTaken(flightId, seatNo);
        }

        /// <summary>
        /// Tries to assign a seat to a passenger.
        /// </summary>
        /// <returns>True if seat assignment was successful; otherwise false.</returns>
        public bool AssignSeat(int passengerId, string seatNo, int flightId)
        {
            if (string.IsNullOrWhiteSpace(seatNo))
                throw new ArgumentException("Seat number cannot be empty.");

            if (_seatRepository.IsSeatTaken(flightId, seatNo))
                return false;

            return _seatRepository.AssignSeatToPassenger(passengerId, seatNo, flightId);
        }

        /// <summary>
        /// Gets the seat assigned to a specific passenger.
        /// </summary>
        public Seat? GetSeatForPassenger(int passengerId, int flightId)
        {
            return _seatRepository.GetSeatByPassenger(passengerId, flightId);
        }

        /// <summary>
        /// Gets a seat object by seat number and flight ID.
        /// </summary>
        public Seat? GetSeatDetails(string seatNo, int flightId)
        {
            return _seatRepository.GetBySeatNo(seatNo, flightId);
        }
    }
}

