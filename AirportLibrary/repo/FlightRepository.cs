using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLibrary.model;

namespace AirportLibrary.repo
{
    public class FlightRepository: IFlightRepository
    {
        private readonly Database _db;

        public FlightRepository(Database db)
        {
            _db = db;
        }
        /// <summary>
        /// Өгөгдлийн сангаас бүх нислэгийг авна.
        /// </summary>
        /// <returns>Боломжтой бүх нислэгийг илэрхийлэх <see cref="Flight"/> объектуудын жагсаалт.</returns>
        public List<Flight> GetAll()
        {
            var flights = new List<Flight>();
            using var conn = _db.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, FlightCode, Status FROM Flights";
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                flights.Add(new Flight
                {
                    Id = reader.GetInt32(0),
                    FlightCode = reader.GetString(1),
                    Status = reader.GetString(2),
                });

            }
            return flights;
        }
        /// <summary>
        /// Нислэгийг өвөрмөц ID-аар нь татаж авна
        /// </summary>
        /// <param name="id">Татаж авах нислэгийн ID</param>
        /// <returns>
        /// Хэрэв олдвол <see cref="Flight"/> объект; Үгүй бол <c>null</c>.
        /// </returns>
        public Flight? GetById(int id)
        {
            using var conn =_db.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, FlightCode, Status FROM Flights WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read()) {
                return new Flight
                {
                    Id = reader.GetInt32(0),
                    FlightCode = reader.GetString(1),
                    Status = reader.GetString(2),
                };
            }
            return null;
        }
        /// <summary>
        /// Өгөгдлийн сан дахь нислэгийн статусыг ID дээр үндэслэн шинэчилнэ.
        /// </summary>
        /// <param name="flight">Шинэчлэгдсэн статус болон нислэгийн ID-г агуулсан <see cref="Flight"/> объект.</param>
        public void Update(Flight flight)
        {
            using var conn = _db.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "Update Flights SET Status = @status WHERE Id = @id";
            cmd.Parameters.AddWithValue("@status", flight.Status);
            cmd.Parameters.AddWithValue("@id", flight.Id);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Нислэгийг нислэгийн кодоор нь татаж авна.
        /// </summary>
        /// <param name="flightCode">Хайх нислэгийн код.</param>
        /// <returns>
        /// Хэрэв олдвол <see cref="Flight"/> объект; Үгүй бол <c>null</c>.
        /// </returns>
        public Flight? GetByCode(string flightCode)
        {
            using var conn = _db.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, FlightCode, Status FROM Flights WHERE FlightCode=@flightCode";
            cmd.Parameters.AddWithValue("@flightCode", flightCode);
            using var reader = cmd.ExecuteReader();

            if (reader.Read()) {
                return new Flight
                {
                    Id = reader.GetInt32(0),
                    FlightCode = reader.GetString(1),
                    Status = reader.GetString(2),   
                };
            }
            return null;
        }
    }
}
