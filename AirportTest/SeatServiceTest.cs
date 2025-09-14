using Moq;
using AirportLibrary.model;
using AirportLibrary.repo;
using Airport.services;


namespace AirportTest
{
    [TestClass]
    public class SeatServiceTest
    {
        private Mock<ISeatRepository> _mockRepo;
        private SeatService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<ISeatRepository>();
            _service = new SeatService(_mockRepo.Object);
        }

        [TestMethod]
        public void GetAvailableSeats_ReturnsSeats()
        {
            var seats = new List<Seat>
            {
                new Seat { Id = 1, SeatNo = "1A", FlightId = 101 },
                new Seat { Id = 2, SeatNo = "1B", FlightId = 101 }
            };
            _mockRepo.Setup(r => r.GetAvailableSeats(101)).Returns(seats);

            var result = _service.GetAvailableSeats(101);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("1A", result[0].SeatNo);
        }

        [TestMethod]
        public void IsSeatAlreadyTaken_WhenTaken_ReturnsTrue()
        {
            _mockRepo.Setup(r => r.IsSeatTaken(101, "1A")).Returns(true);

            var result = _service.IsSeatAlreadyTaken(101, "1A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsSeatAlreadyTaken_WhenNotTaken_ReturnsFalse()
        {
            _mockRepo.Setup(r => r.IsSeatTaken(101, "1A")).Returns(false);

            var result = _service.IsSeatAlreadyTaken(101, "1A");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllSeats_ReturnsAllSeats()
        {
            var seats = new List<Seat>
            {
                new Seat { Id = 1, SeatNo = "1A", FlightId = 101 },
                new Seat { Id = 2, SeatNo = "1B", FlightId = 101 }
            };
            _mockRepo.Setup(r => r.GetAllSeats(101)).Returns(seats);

            var result = _service.GetAllSeats(101);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AssignSeat_EmptySeatNo_ThrowsException()
        {
            _service.AssignSeat(1, "", 101);
        }

        [TestMethod]
        public void AssignSeat_WhenSeatAlreadyTaken_ReturnsFalse()
        {
            _mockRepo.Setup(r => r.IsSeatTaken(101, "1A")).Returns(true);

            var result = _service.AssignSeat(1, "1A", 101);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AssignSeat_WhenSeatAvailable_ReturnsTrue()
        {
            _mockRepo.Setup(r => r.IsSeatTaken(101, "1A")).Returns(false);
            _mockRepo.Setup(r => r.AssignSeatToPassenger(1, "1A", 101)).Returns(true);

            var result = _service.AssignSeat(1, "1A", 101);

            Assert.IsTrue(result);
            _mockRepo.Verify(r => r.AssignSeatToPassenger(1, "1A", 101), Times.Once);
        }

        [TestMethod]
        public void GetSeatForPassenger_WhenExists_ReturnsSeat()
        {
            var seat = new Seat { Id = 1, SeatNo = "1A", FlightId = 101 };
            _mockRepo.Setup(r => r.GetSeatByPassenger(1, 101)).Returns(seat);

            var result = _service.GetSeatForPassenger(1, 101);

            Assert.IsNotNull(result);
            Assert.AreEqual("1A", result.SeatNo);
        }

        [TestMethod]
        public void GetSeatDetails_WhenExists_ReturnsSeat()
        {
            var seat = new Seat { Id = 2, SeatNo = "1B", FlightId = 101 };
            _mockRepo.Setup(r => r.GetBySeatNo("1B", 101)).Returns(seat);

            var result = _service.GetSeatDetails("1B", 101);

            Assert.IsNotNull(result);
            Assert.AreEqual("1B", result.SeatNo);
        }

        [TestMethod]
        public void GetSeatDetails_WhenNotExists_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetBySeatNo("2C", 101)).Returns((Seat)null);

            var result = _service.GetSeatDetails("2C", 101);

            Assert.IsNull(result);
        }
    }
}
