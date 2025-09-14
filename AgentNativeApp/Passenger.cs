using Airport.CheckUI;
using Airport.services;
using AirportLibrary.model;
using AirportLibrary.services;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
namespace AgentNativeApp
{
    public partial class Passenger : Form
    {
        private Label lblFlightStatus;
        private readonly SeatService _seatService;

        private readonly HttpClient _http = new();
        private Button btnPrintBP;
        private PassengerDto? _passenger;
        private Flight? currentFlight;

        public Passenger(SeatService seatService)
        {
            InitializeComponent();
            InitializeLayout();
            _seatService = seatService; 
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

            btnPrintBP = new Button
            {
                Text = "Print Boarding Pass",
                Location = new Point(20, 50),
                Size = new Size(150, 30),
                Enabled = false // Эхэндээ идэвхгүй
            };
            btnPrintBP.Click += BtnPrintBP_Click;
            Controls.Add(btnPrintBP);
        }

        private void BtnPrintBP_Click(object sender, EventArgs e)
        {
            if (_passenger == null || string.IsNullOrWhiteSpace(_passenger.SeatNo) || currentFlight == null)
            {
                MessageBox.Show("Суудал болон нислэгийг зөв сонгоно уу.");
                return;
            }

            var bpPrinter = new BoardingPassPrint(
                _passenger.PassportNo,
                _passenger.SeatNo,
                currentFlight.FlightCode,
                _passenger.FullName
            );

            bpPrinter.Print();
            MessageBox.Show("Boarding pass хэвлэгдлээ!");
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

            if (!string.IsNullOrWhiteSpace(_passenger.SeatNo))
            {
                MessageBox.Show($"Танд {_passenger.SeatNo} суудал аль хэдийн оноогдсон байна!");
             
                return;
            }

            // Нислэгийн төлөв "Бүртгэж байна" эсэхийг шалгах
            if (passenger.FlightStatus != "Бүртгэж байна")
            {
                MessageBox.Show($"Нислэгийн төлөв: {passenger.FlightStatus}. Суудал оноох боломжгүй.");
                return;
            }

            var seatForm = new Seat(passenger, _seatService);
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

            button2.Enabled = false;
            btnPrintBP.Enabled = false;

            //PassengerDto? passenger = null;

            try
            {
                var passenger = await _http.GetFromJsonAsync<PassengerDto>(
                    $"https://localhost:7221/api/Passenger/{passport}");
                if (passenger == null)
                {
                    MessageBox.Show("Passenger not found.");
                    return;
                }

                _passenger = passenger;

                currentFlight = await _http.GetFromJsonAsync<Flight>($"https://localhost:7221/api/Flight/{_passenger.FlightId}");
                // Нислэгийн төлөв харуулах
                lblFlightStatus.Visible = true;
                lblFlightStatus.Text = $"Нислэгийн төлөв: {_passenger?.FlightStatus}";

                // Суудлын мэдээлэл
                lblAssignedSeat.Text = !string.IsNullOrWhiteSpace(_passenger.SeatNo)
                    ? $"Оноосон суудал: {_passenger.SeatNo}"
                    : "Оноосон суудал байхгүй";
                button2.Tag = passenger;

                btnPrintBP.Enabled = !string.IsNullOrWhiteSpace(_passenger.SeatNo);

                // Зөвхөн "Бүртгэж байна" үед болон суудал оноогдоогүй үед суудал оноох боломжтой болгоно
                button2.Enabled = passenger.FlightStatus == "Бүртгэж байна" && string.IsNullOrWhiteSpace(passenger.SeatNo);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"API дуудах үед алдаа гарлаа: {ex.Message}");
                button2.Enabled = false;
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {  }
    }
}
