using AirportLibrary.model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace AirportLibrary.repo;

public class SeatRepository : ISeatRepository
{
    private readonly Database _db;

    public SeatRepository(Database db)
    {
        _db = db;
    }

    /// <summary>
    /// Суудлын дугаар дээр үндэслэн нислэгийн тодорхой суудлыг олох
    /// </summary>
    /// <param name="seatNo"></param>
    /// <param name="flightId"></param>
    /// <returns></returns>
    public Seat? GetBySeatNo(string seatNo, int flightId)
    {
        using var conn = _db.CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, SeatNo, FlightId, isTaken FROM Seat WHERE SeatNo = @seatNo AND FlightId = @flightId";
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
                isTaken = reader.GetInt32(3) == 1
            };
        }

        return null;
    }
    /// <summary>
    /// /// Нислэгийн боломжтой (аваагүй) суудлын жагсаалтыг гаргана.
    /// </summary>
    /// <param name="flightId"></param>
    /// <returns></returns>
    public List<Seat> GetAvailableSeats(int flightId)
    {
        var seats = new List<Seat>();
        using var conn = _db.CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, SeatNo, FlightId, isTaken FROM Seat WHERE FlightId = @flightId AND isTaken = 0";
        cmd.Parameters.AddWithValue("@flightId", flightId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            seats.Add(new Seat
            {
                Id = reader.GetInt32(0),
                SeatNo = reader.GetString(1),
                FlightId = reader.GetInt32(2),
                isTaken = reader.GetInt32(3) == 1
            });
        }

        return seats;
    }

    /// <summary>
    /// Нислэгийн бүх суудлуудыг авна (боломжтой болон эзлэгдсэн)
    /// </summary>
    /// <param name="flightId"></param>
    /// <returns></returns>
    public List<Seat> GetAllSeats(int flightId)
    {
        var seats = new List<Seat>();
        using var conn = _db.CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, SeatNo, FlightId, isTaken FROM Seat WHERE FlightId = @flightId ORDER BY SeatNo";
        cmd.Parameters.AddWithValue("@flightId", flightId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            seats.Add(new Seat
            {
                Id = reader.GetInt32(0),
                SeatNo = reader.GetString(1),
                FlightId = reader.GetInt32(2),
                isTaken = reader.GetInt32(3) == 1
            });
        }

        return seats;
    }

    /// <summary>
    /// /// Нислэгт аль хэдийн тодорхой суудал авсан эсэхийг шалгана.
    /// </summary>
    /// <param name="flightId"></param>
    /// <param name="seatNo"></param>
    /// <returns></returns>
    public bool IsSeatTaken(int flightId, string seatNo)
    {
        using var conn = _db.CreateConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(1) FROM Seat WHERE FlightId = @flightId AND SeatNo = @seatNo AND isTaken = 1";
        cmd.Parameters.AddWithValue("@flightId", flightId);
        cmd.Parameters.AddWithValue("@seatNo", seatNo);

        var result = (long)cmd.ExecuteScalar();
        return result > 0;
    }

    /// <summary>
    /// /// Суудал нь бэлэн байгаа бөгөөд нислэгийн статус зөвшөөрсөн тохиолдолд зорчигчдод суудал онооно.
    /// </summary>
    /// <param name="passengerId"></param>
    /// <param name="seatNo"></param>
    /// <param name="flightId"></param>
    /// <returns></returns>
    public bool AssignSeatToPassenger(int passengerId, string seatNo, int flightId)
    {
        using var conn = _db.CreateConnection();
        using var transaction = conn.BeginTransaction();

        // Flight статус шалгах
        using var checkFlightCmd = conn.CreateCommand();
        checkFlightCmd.CommandText = "SELECT Status FROM Flights WHERE Id = @flightId";
        checkFlightCmd.Parameters.AddWithValue("@flightId", flightId);
        var status = (string?)checkFlightCmd.ExecuteScalar();

        if (string.IsNullOrEmpty(status) || status != "Бүртгэж байна")
            return false;  // Нислэг олдоогүй эсвэл бүртгэх боломжгүй

        // Суудал эзлэгдсэн эсэх шалгах
        var checkSeat = conn.CreateCommand();
        checkSeat.CommandText = "SELECT isTaken FROM Seat WHERE FlightId = @flightId AND SeatNo = @seatNo";
        checkSeat.Parameters.AddWithValue("@flightId", flightId);
        checkSeat.Parameters.AddWithValue("@seatNo", seatNo);

        var isTaken = (long)checkSeat.ExecuteScalar();
        if (isTaken == 1)
            return false;  // аль хэдийн суудал эзэлсэн

        // Суудлыг IsTaken болгож оноох
        var updateSeat = conn.CreateCommand();
        updateSeat.CommandText = "UPDATE Seat SET isTaken = 1 WHERE FlightId = @flightId AND SeatNo = @seatNo";
        updateSeat.Parameters.AddWithValue("@flightId", flightId);
        updateSeat.Parameters.AddWithValue("@seatNo", seatNo);
        updateSeat.ExecuteNonQuery();

        // Passenger-д суудал оноох
        var updatePassenger = conn.CreateCommand();
        updatePassenger.CommandText = "UPDATE Passenger SET SeatNo = @seatNo WHERE Id = @passengerId";
        updatePassenger.Parameters.AddWithValue("@seatNo", seatNo);
        updatePassenger.Parameters.AddWithValue("@passengerId", passengerId);
        updatePassenger.ExecuteNonQuery();

        transaction.Commit();
        return true;
    }
    /// <summary>
    /// /// Тодорхой нислэгийн зорчигчдод хуваарилагдсан суудлыг авна.
    /// </summary>
    /// <param name="passengerId"></param>
    /// <param name="flightId"></param>
    /// <returns></returns>
    public Seat? GetSeatByPassenger(int passengerId, int flightId)
    {
        using var conn = _db.CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
                SELECT Id, SeatNo, FlightId, isTaken 
                FROM Seat
                WHERE SeatNo = (SELECT SeatNo FROM Passenger WHERE Id = @passengerId) 
                AND FlightId = @flightId AND SeatNo IS NOT NULL";

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
                isTaken = reader.GetInt32(3) == 1
            };
        }

        return null;
    }

    /// <summary>
    /// Нислэгт суудлуудыг автоматаар үүсгэх
    /// </summary>
    /// <param name="flightId">Нислэгийн ID</param>
    /// <param name="totalSeats">Суудлын тоо</param>
    public void CreateSeatsForFlight(int flightId, int totalSeats)
    {
        using var conn = _db.CreateConnection();
        using var transaction = conn.BeginTransaction();

        try
        {
            // Суудлуудыг үүсгэх (жишээ: 1A, 1B, 1C, 2A, 2B, 2C...)
            for (int i = 1; i <= totalSeats; i++)
            {
                var row = (i - 1) / 3 + 1; // 3 суудал нэг эгнээнд
                var seatLetter = (char)('A' + (i - 1) % 3); // A, B, C
                var seatNo = $"{row}{seatLetter}";

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Seat (SeatNo, FlightId, isTaken) 
                    VALUES (@seatNo, @flightId, 0)";
                
                cmd.Parameters.AddWithValue("@seatNo", seatNo);
                cmd.Parameters.AddWithValue("@flightId", flightId);
                cmd.ExecuteNonQuery();
            }

            transaction.Commit();
            Console.WriteLine($"✅ Flight {flightId}-д {totalSeats} суудал амжилттай үүсгэгдлээ");
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"❌ Суудлууд үүсгэх үед алдаа: {ex.Message}");
            throw;
        }
    }
}
