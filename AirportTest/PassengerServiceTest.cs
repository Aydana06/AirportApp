using Moq;
using AirportLibrary.repo;
using AirportLibrary.model;
using AirportLibrary.services;

namespace AirportTest
{
    [TestClass]
    public class PassengerServiceTest
    {
        private Mock<IPassengerRepository> _mockRepo;
        private PassengerService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IPassengerRepository>();
            _service = new PassengerService(_mockRepo.Object);
        }

        [TestMethod]
        public void GetPassengerByPassport_ExistingPassport_ReturnsPassenger()
        {
            var passenger = new Passenger { Id = 1, Name = "John Doe", PassportNo = "AB123456" };
            _mockRepo.Setup(r => r.GetByPassport("AB123456")).Returns(passenger);

            var result = _service.GetPassengerByPassport("AB123456");

            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.Name);
        }

        [TestMethod]
        public void GetPassengerByPassport_NonExistingPassport_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByPassport("XX000000")).Returns((Passenger)null);

            var result = _service.GetPassengerByPassport("XX000000");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPassengerByFlight_ValidFlightId_ReturnsPassengers()
        {
            var passengers = new List<Passenger>
            {
                new Passenger { Id = 1, Name = "Alice", PassportNo = "P123" },
                new Passenger { Id = 2, Name = "Bob", PassportNo = "P456" }
            };
            _mockRepo.Setup(r => r.GetPassengerByFlight(100)).Returns(passengers);

            var result = _service.GetPassengerByFlight(100);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Alice", result[0].Name);
        }

        [TestMethod]
        public void IsPassengerRegistered_PassengerExists_ReturnsTrue()
        {
            var passenger = new Passenger { Id = 1, Name = "Jane Doe", PassportNo = "YY999999" };
            _mockRepo.Setup(r => r.GetByPassport("YY999999")).Returns(passenger);

            var result = _service.IsPassengerRegistered("YY999999");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPassengerRegistered_PassengerDoesNotExist_ReturnsFalse()
        {
            _mockRepo.Setup(r => r.GetByPassport("NO000000")).Returns((Passenger)null);

            var result = _service.IsPassengerRegistered("NO000000");

            Assert.IsFalse(result);
        }
    }
}
