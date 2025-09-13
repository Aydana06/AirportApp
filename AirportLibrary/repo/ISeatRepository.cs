using AirportLibrary.model;
using System.Collections.Generic;

namespace AirportLibrary.repo
{
    public interface ISeatRepository
    {
        /// <summary>
        /// Суудлын дугаар дээр үндэслэн нислэгийн тодорхой суудлыг олох.
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Seat? GetBySeatNo(string seatNo, int flightId);

        /// <summary>
        /// Нислэгийн боломжтой (аваагүй) суудлын жагсаалтыг гаргана.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        List<Seat> GetAvailableSeats(int flightId);

        /// <summary>
        /// Нислэгийн бүх суудлуудыг авна (боломжтой болон эзлэгдсэн)
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        List<Seat> GetAllSeats(int flightId);

        /// <summary>
        /// Нислэгт аль хэдийн тодорхой суудал авсан эсэхийг шалгана.
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        bool IsSeatTaken(int flightId, string seatNo);

        /// <summary>
        /// Суудал нь бэлэн байгаа бөгөөд нислэгийн статус зөвшөөрсөн тохиолдолд зорчигчдод суудал онооно.
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="seatNo"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        bool AssignSeatToPassenger(int passengerId, string seatNo, int flightId);

        /// <summary>
        /// Тодорхой нислэгийн зорчигчдод хуваарилагдсан суудлыг авна.
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        Seat? GetSeatByPassenger(int passengerId, int flightId);

        /// <summary>
        /// Нислэгт суудлуудыг автоматаар үүсгэх
        /// </summary>
        /// <param name="flightId">Нислэгийн ID</param>
        /// <param name="totalSeats">Суудлын тоо</param>
        void CreateSeatsForFlight(int flightId, int totalSeats);
    }
}
