using AirportLibrary.services;

namespace AgentNativeApp
{
    public partial class FlightStatus : Form
    {
        private readonly FlightService _flightService;
        public FlightStatus(FlightService flightService)
        {
            InitializeComponent();
            _flightService = flightService;
        }

        private void button4_Click(object sender, EventArgs e)
        { }

        private void Passenger_Load(object sender, EventArgs e)
        {  }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BtnSave_Click(sender, e);
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            string flightCode = FlightCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(flightCode))
            {
                MessageBox.Show("Нислэгийн код хоосон байна.");
                return;
            }   

            string status = StatusBox.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("Төлөв сонгоно уу.");
                return;
            }

            try
            {
                var flight = _flightService.GetFlightByCode(flightCode);
                if(flight == null)
                {
                    MessageBox.Show($"Flight {flightCode} олдсонгүй.");
                    return;
                }

                _flightService.UpdateFlightStatus(flight.Id, status);

                lblStatus.Text = "✅ Амжилттай шинэчлэгдлээ.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ Алдаа гарлаа: " + ex.Message;
            }
        }
    }
}
