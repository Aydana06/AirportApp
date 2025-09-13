using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLibrary.model;

namespace AirportLibrary.repo
{
    public class PassengerRepository: IPassengerRepository
    {
        private readonly Database _db;
        public PassengerRepository(Database db)
        {
            _db = db;
        }
        /// <summary>
        /// Зорчигчийг паспортын дугаараар нь татаж авна.
        /// </summary>
        /// <param name="passportNo">Зорчигчийн паспортын дугаар.</param>
        /// <returns>Тохирох бичлэг олдвол <see cref="Зорчигч"/> объект; Үгүй бол <c>null</c>.</returns>
        public Passenger? GetByPassport(string passportNo)
        {
            using var conn = _db.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Passenger WHERE PassportNo = @passportNo";
            cmd.Parameters.AddWithValue("@passportNo", passportNo);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Passenger
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PassportNo = reader.GetString(2),
                    FlightId = reader.GetInt32(3),
                    SeatNo = reader.IsDBNull(4) ? null: reader.GetString(4)
                };
            }
            return null;
        }

        /// <summary>
        /// Тодорхой нислэгтэй холбоотой бүх зорчигчдыг татаж авна.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID.</param>
        /// <returns>Заасан нислэгт хуваарилагдсан <see cref="Зорчигч"/> объектуудын жагсаалт.</returns>
        public List<Passenger> GetPassengerByFlight(int flightId)
        {
            var passengers = new List<Passenger>();
            using var conn = _db.CreateConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Passenger WHERE FlightId = @flightId";
            cmd.Parameters.AddWithValue("@flightId", flightId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                passengers.Add(new Passenger
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PassportNo = reader.GetString(2),
                    FlightId = reader.GetInt32(3),
                    SeatNo = reader.IsDBNull(5) ? null : reader.GetString(5),
                });
            }
            return passengers;
        }
    }
}
