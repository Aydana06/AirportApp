using Airport.services;
using AirportLibrary.model;
using AirportLibrary.services;
using System.Net.Http.Json;
namespace AgentNativeApp
{
    public partial class Passenger : Form
    {
        private Label lblFlightStatus;
        private readonly SeatService _seatService;
        private readonly FlightService _flightService;
        private readonly PassengerService _passengerService;

        public Passenger(SeatService seatService, FlightService flightService, PassengerService passengerService)
        {
            InitializeComponent();
            InitializeLayout();
            _seatService = seatService;
            _flightService = flightService;
            _passengerService = passengerService;
        }

        private void InitializeLayout()
        {
            lblFlightStatus = new Label
            {
                Text = "Нислэгийн төлөв: ачааллаж байна...",
                Location = new Point(20, 10),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Visible = false
            };
            Controls.Add(lblFlightStatus);
        }

        private void Passenger_Load(object sender, EventArgs e)
        { }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Tag is not PassengerDto passenger)
            {
                MessageBox.Show("Эхлээд зорчигчийн мэдээллийг шалгана уу.");
                return;
            }

            // Аль хэдийн суудал оноогдсон эсэхийг шалгах
            if (!string.IsNullOrWhiteSpace(passenger.SeatNo))
            {
                MessageBox.Show($"Танд {passenger.SeatNo} суудал аль хэдийн оноогдсон байна!");
                return;
            }

            // Нислэгийн төлөв "Бүртгэж байна" эсэхийг шалгах
            if (passenger.FlightStatus != "Бүртгэж байна")
            {
                MessageBox.Show($"Нислэгийн төлөв: {passenger.FlightStatus}. Суудал оноох боломжгүй.");
                return;
            }

            var seatForm = new Seat(passenger, _seatService, _flightService);
            seatForm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            LoadPassengerByPassport(sender, EventArgs.Empty);
        }

        private async void LoadPassengerByPassport(object? sender, EventArgs e)
        {
            var passport = PassportNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(passport)) return;

            try
            {
                var passengerEntity = _passengerService.GetPassengerByPassport(passport);
                if (passengerEntity == null)
                {
                    MessageBox.Show("Passenger олдсонгүй.");
                    button2.Enabled = false;
                    return;
                }

                var flight = _flightService.GetFlightById(passengerEntity.FlightId);

                var passenger = _passengerService.GetPassengerDtoByPassport(passport);
                if (passenger == null)
                {
                    MessageBox.Show("Passenger олдсонгүй.");
                    button2.Enabled = false;
                    return;
                }

                // Нислэгийн төлөв харуулах
                lblFlightStatus.Visible = true;
                lblFlightStatus.Text = $"Нислэгийн төлөв: {passenger?.FlightStatus}";

                // Суудлын мэдээлэл
                lblAssignedSeat.Text = !string.IsNullOrWhiteSpace(passenger.SeatNo)
                    ? $"Оноосон суудал: {passenger.SeatNo}"
                    : "Оноосон суудал байхгүй";

                button2.Tag = passenger;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Өгөгдөл татах үед алдаа гарлаа: {ex.Message}");
                button2.Enabled = false;
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {  }
    }
}
