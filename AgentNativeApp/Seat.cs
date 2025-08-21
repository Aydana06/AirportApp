using AirportLibrary.model;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentNativeApp
{
    public partial class Seat : Form
    {
        private readonly string _passport;
        private readonly HttpClient _http = new();
        private List<Flight> flights = new();
        private List<AirportLibrary.model.Seat> seats = new();
        private Flight currentFlight = null;

        public Seat(string passport)
        {
            _passport = passport;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        { }
        private void button1_Click(object sender, EventArgs e)
        { }
        private void btnChangeStatus_Click(object sender, EventArgs e)
        { }
        private void button2_Click(object sender, EventArgs e)
        { }
        private void Seat_Load(object sender, EventArgs e)
        {
            lblPassport.Text = _passport;
            LoadFlights();
            GenerateFlights();
        }

        private async void LoadFlights()
        {
            await LoadFlightsAsync();
        }
        // API-аас Flight авах
        private async Task LoadFlightsAsync()
        {
            flights = await _http.GetFromJsonAsync<List<Flight>>("https://localhost:7221/api/Flight") ?? new List<Flight>();
        }

        // API-аас Seats авах
        private async Task LoadSeatsAsync(int flightId)
        {
            seats = await _http.GetFromJsonAsync<List<AirportLibrary.model.Seat>>($"https://localhost:7221/api/Flight/{flightId}/Seats") ?? new List<AirportLibrary.model.Seat>();
        }
        private void GenerateFlights()
        {
            int startX = 130;
            int startY = 30;
            int btnWidth = 70;
            int btnHeight = 30;
            int margin = 10;
            int col = 0;

            foreach (var flight in flights)
            {
                Button flightBtn = new Button();
                flightBtn.Text = flight.FlightCode;
                flightBtn.Width = btnWidth;
                flightBtn.Height = btnHeight;
                flightBtn.Left = startX + (col * (btnWidth + margin));
                flightBtn.Top = startY;
                flightBtn.Tag = flight;
                flightBtn.Click += FlightBtn_Click;

                this.Controls.Add(flightBtn);
                col++;
            }
        }
        private async void FlightBtn_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            currentFlight = btn.Tag as Flight;
            if (currentFlight == null) return;

            ClearSeats();
            await LoadSeatsAsync(currentFlight.Id);
            GenerateSeats();
        }

        private void ClearSeats()
        {
            var removeList = new List<Control>();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button seatBtn && seatBtn.Tag is AirportLibrary.model.Seat)
                {
                    removeList.Add(ctrl);
                }
            }

            foreach (var ctrl in removeList)
            {
                this.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void GenerateSeats()
        {
            int startX = 35;
            int startY = 130;
            int btnWidth = 50;
            int btnHeight = 40;
            int margin = 10;

            int col = 0;
            int row = 0;

            foreach (var seat in seats)
            {
                Button seatBtn = new Button();
                seatBtn.Text = seat.SeatNo;
                seatBtn.Width = btnWidth;
                seatBtn.Height = btnHeight;

                seatBtn.BackColor = seat.isTaken ? Color.Red : Color.LightGreen;

                seatBtn.Tag = seat;
                seatBtn.Click += SeatBtn_Click;

                seatBtn.Left = startX + (col * (btnWidth + margin));
                seatBtn.Top = startY + (row * (btnHeight + margin));

                this.Controls.Add(seatBtn);

                col++;
                if (col > 10)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private async void SeatBtn_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            var seat = btn.Tag as AirportLibrary.model.Seat;
            if (seat == null || currentFlight == null) return;

            // Суудал захиалах хүсэлт үүсгэх
            var checkInRequest = new
            {
                passportNo = _passport,
                seatNo = seat.SeatNo,
                flightId = currentFlight.Id
            };

            var response = await _http.PostAsJsonAsync("https://localhost:7221/api/CheckIn", checkInRequest);

            if (response.IsSuccessStatusCode)
            {
                seat.isTaken = true;
                btn.BackColor = Color.Red;
                MessageBox.Show($"Суудал {seat.SeatNo} амжилттай оноосон!");
            }
            else
            {
                MessageBox.Show("Суудал оноох үед алдаа гарлаа!");
            }

        }

        //private void btnSeat_Click(object sender, EventArgs e)
        //{

        //}

        private void btnSeat_Click_1(object sender, EventArgs e)
        {
            //Суудал оноох 
        }
    }
}
