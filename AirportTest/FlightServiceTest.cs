using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AirportLibrary.repo;
using AirportLibrary.model;
using AirportLibrary.services;
using System;
using System.Collections.Generic;

namespace AirportTest
{
    [TestClass]
    public class FlightServiceTest
    {
        private Mock<IFlightRepository> _mockRepo;

        private FlightService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IFlightRepository>();
            _service = new FlightService(_mockRepo.Object);
        }

        [TestMethod]
        public void UpdateFlightStatus_ValidId_UpdatesStatus()
        {
            var flight = new Flight { Id = 1, FlightCode = "MN121", Status = "Бүртгэж байна" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(flight);

            _service.UpdateFlightStatus(1, "Ниссэн");

            Assert.AreEqual("Ниссэн", flight.Status);
            _mockRepo.Verify(r => r.Update(flight), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UpdateFlightStatus_InvalidId_ThrowsException()
        {
            _mockRepo.Setup(r => r.GetById(99)).Returns((Flight)null);

            _service.UpdateFlightStatus(99, "Ниссэн");
        }

        [TestMethod]
        public void GetFlightById_ValidId_ReturnsFlight()
        {
            var flight = new Flight { Id = 1, FlightCode = "MN121", Status = "Бүртгэж байна" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(flight);

            var result = _service.GetFlightById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("MN121", result.FlightCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFlightById_InvalidId_ThrowsException()
        {
            var repo = new Mock<IFlightRepository>();
            var service = new FlightService(repo.Object);

            service.GetFlightById(0);
        }

        [TestMethod]
        public void GetFlightByCode_ExistingCode_ReturnsFlight()
        {
            var flight = new Flight { Id = 1, FlightCode = "MN121", Status = "Бүртгэж байна" };
            _mockRepo.Setup(r => r.GetByCode("MN121")).Returns(flight);

            var result = _service.GetFlightByCode("MN121");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void GetFlightByCode_NonExistingCode_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByCode("ZZ090")).Returns((Flight)null);

            var result = _service.GetFlightByCode("ZZ090");

            Assert.IsNull(result);
        }
    }
}
