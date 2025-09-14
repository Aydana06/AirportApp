
using AirportLibrary.model;
using AirportLibrary.repo;

namespace Airport.services
{
    public class SeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        /// <summary>
        /// Тухайн нислэгийн боломжтой бүх суудлыг татаж авна.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public List<Seat> GetAvailableSeats(int flightId)
        {
            return _seatRepository.GetAvailableSeats(flightId);
        }

        /// <summary>
        /// Нислэгт аль хэдийн тодорхой суудал авсан эсэхийг шалгана
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public bool IsSeatAlreadyTaken(int flightId, string seatNo)
        {
            return _seatRepository.IsSeatTaken(flightId, seatNo);
        }

        /// <summary>
        /// Нислэгийн бүх суудлуудыг авна (боломжтой болон эзлэгдсэн)
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public List<Seat> GetAllSeats(int flightId)
        {
            return _seatRepository.GetAllSeats(flightId);
        }

        /// <summary>
        /// Зорчигчдод суудал оноохыг оролдоно.
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="seatNo"></param>
        /// <param name="flightId"></param>
        /// <returns>Суудлын хуваарилалт амжилттай болсон бол true; үгүй бол false</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool AssignSeat(int passengerId, string seatNo, int flightId)
        {
            if (string.IsNullOrWhiteSpace(seatNo))
                throw new ArgumentException("Seat number cannot be empty.");

            if (_seatRepository.IsSeatTaken(flightId, seatNo))
                return false;

            return _seatRepository.AssignSeatToPassenger(passengerId, seatNo, flightId);
        }

        /// <summary>
        /// Тодорхой зорчигчдод хуваарилагдсан суудлыг авна.
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public Seat? GetSeatForPassenger(int passengerId, int flightId)
        {
            return _seatRepository.GetSeatByPassenger(passengerId, flightId);
        }

        /// <summary>
        /// Суудлын дугаар болон нислэгийн ID-аар суудлын объектыг авна.
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public Seat? GetSeatDetails(string seatNo, int flightId)
        {
            return _seatRepository.GetBySeatNo(seatNo, flightId);
        }
    }
}

