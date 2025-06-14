using AirportLibrary.model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace AirportLibrary.repo
{
    public class SeatRepository
    {
        private readonly Database _context;

        // SQL Query-үүдийг төвлөрүүлэх
        private const string GetSeatBySeatNoQuery = "SELECT Id, SeatNo, FlightId, IsTaken FROM Seats WHERE SeatNo = @seatNo AND FlightId = @flightId";
        private const string GetAvailableSeatsQuery = "SELECT Id, SeatNo, FlightId, IsTaken FROM Seats WHERE FlightId = @flightId AND IsTaken = 0";
        private const string IsSeatTakenQuery = "SELECT COUNT(1) FROM Seats WHERE FlightId = @flightId AND SeatNo = @seatNo AND IsTaken = 1";
        private const string GetSeatByPassengerQuery = @"
            SELECT s.Id, s.SeatNo, s.FlightId, s.IsTaken 
            FROM Seats s
            JOIN Passengers p ON s.SeatNo = p.SeatNo AND s.FlightId = p.FlightId
            WHERE p.Id = @passengerId";

        public SeatRepository(Database context)
        {
            _context = context;
        }

        public Seat? GetBySeatNo(string seatNo, int flightId)
        {
            using var conn = _context.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = GetSeatBySeatNoQuery;
            cmd.Parameters.AddWithValue("@seatNo", seatNo);
            cmd.Parameters.AddWithValue("@flightId", flightId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Seat
                {
                    Id = reader.GetInt32(0),
                    SeatNo = reader.GetString(1),
                    FlightId = reader.GetInt32(2),
                    isTaken = reader.GetBoolean(3)  // 👉 Direct boolean
                };
            }

            return null;
        }

        public List<Seat> GetAvailableSeats(int flightId)
        {
            var seats = new List<Seat>();
            using var conn = _context.CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = GetAvailableSeatsQuery;
            cmd.Parameters.AddWithValue("@flightId", flightId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                seats.Add(new Seat
                {
                    Id = reader.GetInt32(0),
                    SeatNo = reader.GetString(1),
                    FlightId = reader.GetInt32(2),
                    isTaken = reader.GetBoolean(3)
                });
            }

            return seats;
        }

        public bool IsSeatTaken(int flightId, string seatNo)
        {
            using var conn = _context.CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = IsSeatTakenQuery;
            cmd.Parameters.AddWithValue("@flightId", flightId);
            cmd.Parameters.AddWithValue("@seatNo", seatNo);

            var result = (long)cmd.ExecuteScalar();
            return result > 0;
        }

        public bool AssignSeatToPassenger(int passengerId, string seatNo, int flightId)
        {
            using var conn = _context.CreateConnection();
            using var transaction = conn.BeginTransaction();

            try
            {
                // 🔍 Check flight status
                using var checkFlightCmd = conn.CreateCommand();
                checkFlightCmd.Transaction = transaction;
                checkFlightCmd.CommandText = "SELECT Status FROM Flights WHERE Id = @flightId";
                checkFlightCmd.Parameters.AddWithValue("@flightId", flightId);
                var status = (string?)checkFlightCmd.ExecuteScalar();

                if (string.IsNullOrEmpty(status) || status != "Бүртгэж байна")
                {
                    transaction.Rollback();
                    return false;
                }

                // 🔍 Check if seat is already taken
                using var checkCmd = conn.CreateCommand();
                checkCmd.Transaction = transaction;
                checkCmd.CommandText = "SELECT IsTaken FROM Seats WHERE FlightId = @flightId AND SeatNo = @seatNo";
                checkCmd.Parameters.AddWithValue("@flightId", flightId);
                checkCmd.Parameters.AddWithValue("@seatNo", seatNo);
                var isTaken = (long)checkCmd.ExecuteScalar();

                if (isTaken == 1)
                {
                    transaction.Rollback();
                    return false;
                }

                // ✅ Update seat
                using var updateSeat = conn.CreateCommand();
                updateSeat.Transaction = transaction;
                updateSeat.CommandText = "UPDATE Seats SET IsTaken = 1 WHERE FlightId = @flightId AND SeatNo = @seatNo";
                updateSeat.Parameters.AddWithValue("@flightId", flightId);
                updateSeat.Parameters.AddWithValue("@seatNo", seatNo);
                updateSeat.ExecuteNonQuery();

                // ✅ Assign seat to passenger
                using var updatePassenger = conn.CreateCommand();
                updatePassenger.Transaction = transaction;
                updatePassenger.CommandText = "UPDATE Passengers SET SeatNo = @seatNo WHERE Id = @passengerId";
                updatePassenger.Parameters.AddWithValue("@seatNo", seatNo);
                updatePassenger.Parameters.AddWithValue("@passengerId", passengerId);
                updatePassenger.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error assigning seat: {ex.Message}");
                transaction.Rollback();
                return false;
            }
        }

        public Seat? GetSeatByPassenger(int passengerId, int flightId)
        {
            using var conn = _context.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = GetSeatByPassengerQuery;
            cmd.Parameters.AddWithValue("@passengerId", passengerId);
            cmd.Parameters.AddWithValue("@flightId", flightId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Seat
                {
                    Id = reader.GetInt32(0),
                    SeatNo = reader.GetString(1),
                    FlightId = reader.GetInt32(2),
                    isTaken = reader.GetBoolean(3)
                };
            }

            return null;
        }
    }
}
