using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentNativeApp
{
    public partial class Passenger : Form
    {
        private readonly HttpClient _http = new();
        private Label lblFlightStatus;
        private string flightStatus = "Бүртгэж байна";
        public Passenger()
        {
            InitializeComponent();

            InitializeLayout();
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
            var passport = PassportNumber.Text.Trim();

            if (string.IsNullOrWhiteSpace(passport))
            {
                MessageBox.Show("Эхлээд паспортын дугаар хайна уу.");
                return;
            }

            Seat seatForm = new Seat(passport);
            seatForm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            PassportNumber_Leave(sender, EventArgs.Empty);
        }

        private async void PassportNumber_Leave(object? sender, EventArgs e)
        {
            var passport = PassportNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(passport)) return;

            button2.Enabled = false;

            // var passenger = await _http.GetFromJsonAsync<PassengerDto>($"https://localhost:7221/api/Passenger/{passport}");

            try
            {
                var passenger = await _http.GetFromJsonAsync<PassengerDto>(
                    $"https://localhost:7221/api/Passenger/{passport}");
                if (passenger == null)
                {
                    MessageBox.Show("Passenger not found.");
                    return;
                }

                // Нислэгийн төлөв харуулах
                lblFlightStatus.Visible = true;
                lblFlightStatus.Text = $"Нислэгийн төлөв: {flightStatus}";

                // Суудлын мэдээлэл
                lblAssignedSeat.Text = !string.IsNullOrWhiteSpace(passenger.SeatNo)
                    ? $"Оноосон суудал: {passenger.SeatNo}"
                    : "Оноосон суудал байхгүй";
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"API дуудах үед алдаа гарлаа: {ex.Message}");
            }
            finally
            {
                button2.Enabled = true;
            }

            //if (passenger == null)
            //{
            //    lblAssignedSeat.Text = "Зорчигч олдсонгүй";
            //    lblFlightStatus.Visible = false;
            //    return;
            //}

            //lblFlightStatus.Visible = true;
            //lblFlightStatus.Text = $"Нислэгийн төлөв: {flightStatus}";
            //lblAssignedSeat.Text = !string.IsNullOrWhiteSpace(passenger.SeatNo)
            //    ? $"Оноосон суудал: {passenger.SeatNo}"
            //    : "Оноосон суудал байхгүй";
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
    public class PassengerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string PassportNo { get; set; } = "";
        public int FlightId { get; set; }
        public string? SeatNo { get; set; }
    }
}
