using Airport.services;
using AirportLibrary;
using AirportLibrary.repo;
using AirportLibrary.services;

namespace AgentNativeApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string dbPath = "C:\\Users\\Lenovo\\Documents\\3th_COURSE\\AirportApp\\Airport-API\\airport.db";

            var database = new Database(dbPath);

            // Repository-ууд
            var seatRepo = new SeatRepository(database);
            var flightRepo = new FlightRepository(database);
            var passengerRepo = new PassengerRepository(database);

            // Service-ууд
            var seatService = new SeatService(seatRepo);
            var flightService = new FlightService(flightRepo);
            var passengerService = new PassengerService(passengerRepo, flightService);
 
            var flightStatusForm = new FlightStatus(flightService);

            var passengerForm = new Passenger(seatService, flightService, passengerService);
            //var passengerForm1 = new Passenger(seatService);

            flightStatusForm.Show();
            passengerForm.Show();
            //passengerForm1.Show();

            Application.Run();

        }
    }
}