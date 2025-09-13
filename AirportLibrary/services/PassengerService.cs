using AirportLibrary.repo;
using AirportLibrary.model;

namespace AirportLibrary.services
{
    public class PassengerService
    {
        private readonly IPassengerRepository _repo;

        public PassengerService(IPassengerRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Зорчигчийг паспортын дугаараар нь авна.
        /// </summary>
        /// <param name="PassportNo">Зорчигчийн паспортын дугаар.</param>
        /// <returns>Хэрэв олдвол тохирох <see cref="Passenger"/>; үгүй бол <c>null</c>.</returns>
        public Passenger? GetPassengerByPassport(string PassportNo)
        {
            return _repo.GetByPassport(PassportNo);
        }

        /// <summary>
        /// Тодорхой нислэгтэй холбоотой бүх зорчигчдыг авна.
        /// </summary>
        /// <param name="flightId">Нислэгийн ID.</param>
        /// <returns>Нислэгт хуваарилагдсан <see cref="Passenger"/> объектуудын жагсаалт.</returns>
        public List<Passenger> GetPassengerByFlight(int flightId)
        {
            return _repo.GetPassengerByFlight(flightId);
        }

        /// <summary>
        /// Зорчигч өгөгдсөн паспортын дугаараар бүртгүүлсэн эсэхийг шалгана.
        /// </summary>
        /// <param name="passportNo">Шалгах паспортын дугаар.</param>
        /// <returns>xэрэв зорчигч байгаа бол <c>true</c>; үгүй бол <c>false</c>.</returns>
        public bool IsPassengerRegistered(string passportNo) {
            return _repo.GetByPassport(passportNo) != null;
        }

    }
}
