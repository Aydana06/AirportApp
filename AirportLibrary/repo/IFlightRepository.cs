using AirportLibrary.model;
using System.Collections.Generic;

namespace AirportLibrary.repo
{
    /// <summary>
    /// Нислэгийн өгөгдөлтэй ажиллахад зориулсан repository интерфэйс.
    /// </summary>
    public interface IFlightRepository
    {
        /// <summary>
        /// Өгөгдлийн сангаас бүх нислэгийг авна.
        /// </summary>
        /// <returns>Нислэгийн жагсаалт.</returns>
        List<Flight> GetAll();

        /// <summary>
        /// Нислэгийг өвөрмөц ID-аар нь татаж авна.
        /// </summary>
        /// <param name="id">Нислэгийн ID.</param>
        /// <returns>Хэрэв олдвол <see cref="Flight"/> объект; үгүй бол <c>null</c>.</returns>
        Flight? GetById(int id);

        /// <summary>
        /// Нислэгийн статусыг шинэчилнэ.
        /// </summary>
        /// <param name="flight">Шинэчлэгдсэн нислэгийн мэдээлэл.</param>
        void Update(Flight flight);

        /// <summary>
        /// Нислэгийг нислэгийн кодоор нь татаж авна.
        /// </summary>
        /// <param name="flightCode">Нислэгийн код.</param>
        /// <returns>Хэрэв олдвол <see cref="Flight"/> объект; үгүй бол <c>null</c>.</returns>
        Flight? GetByCode(string flightCode);

        /// <summary>
        /// Шинэ нислэг үүсгэх.
        /// </summary>
        /// <param name="flight">Үүсгэх нислэгийн мэдээлэл.</param>
        /// <returns>Үүсгэгдсэн нислэгийн ID.</returns>
        int Create(Flight flight);
    }
}
