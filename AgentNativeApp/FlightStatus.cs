using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace AgentNativeApp
{
    public partial class FlightStatus : Form
    {
        private readonly HttpClient _http = new();
        public FlightStatus()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void Passenger_Load(object sender, EventArgs e)
        {
            // Form load event handler
        }

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

            var request = new
            {
                FlightCode = FlightCode.Text.Trim(),
                Status = status
            };

            var response = await _http.PutAsync("https://localhost:7221/api/Flight/UpdateStatus",
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

            var msg = await response.Content.ReadAsStringAsync();
            lblStatus.Text = response.IsSuccessStatusCode
                ? "✅ Амжилттай шинэчлэгдлээ."
                : "❌ Алдаа гарлаа: " + msg;

        }
    }
}
