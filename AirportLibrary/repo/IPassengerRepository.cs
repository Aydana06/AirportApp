using AirportLibrary.model;
using System.Collections.Generic;

namespace AirportLibrary.repo
{
    public interface IPassengerRepository
    {
        /// <summary>
        /// Зорчигчийг паспортын дугаараар нь татаж авна.
        /// </summary>
        /// <param name="passportNo">Зорчигчийн паспортын дугаар.</param>
        /// <returns>Тохирох бичлэг олдвол <see cref="Passenger"/> объект; Үгүй бол <c>null</c>.</returns>
        Passenger? GetByPassport(string passportNo);

        /// <summary>
        /// Тодорхой нислэгтэй холбоотой бүх зорчигчдыг татаж авна.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID.</param>
        /// <returns>Заасан нислэгт хуваарилагдсан <see cref="Passenger"/> объектуудын жагсаалт.</returns>
        List<Passenger> GetPassengerByFlight(int flightId);
    }
}
