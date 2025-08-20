using AirportLibrary.model;
using Microsoft.Data.Sqlite;
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
        private List<Flight> flights = new List<Flight>();
        private List<AirportLibrary.model.Seat> seats = new List<AirportLibrary.model.Seat>();
        private string connectionString = "Data Source=C:\\Users\\Lenovo\\Documents\\3th_COURSE\\AirportApp\\Airport-API\\airport.db";

        private Flight currentFlight = null;

        public Seat()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        { }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Seat_Load(object sender, EventArgs e)
        {
            LoadFlights();
            GenerateFlights();
        }
        private void LoadFlights()
        {
            flights.Clear();

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, FlightCode, Status FROM Flights";

                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        flights.Add(new Flight
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FlightCode = reader.GetString(reader.GetOrdinal("FlightCode")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }
        }

        private void LoadSeats(int flightId)
        {
            seats.Clear();
            using (var conn = new SqliteConnection(connectionString)) {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, SeatNo, FlightId, isTaken FROM Seat WHERE FlightId=@fid";
                cmd.Parameters.AddWithValue("@fid", flightId);

                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        seats.Add(new AirportLibrary.model.Seat
                        {
                            Id = reader.GetInt32(0),
                            SeatNo = reader.GetString(1),
                            FlightId = reader.GetInt32(2),
                            isTaken = reader.GetInt32(3) == 1

                        });
                    }
                }
            }


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
        private void FlightBtn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button; 
            if (btn == null) return; 
            currentFlight = btn.Tag as Flight; 
            if (currentFlight == null) return; 
            ClearSeats(); 
            LoadSeats(currentFlight.Id); 
            GenerateSeats();
        }

        private void ClearSeats()
        {
            var removeList = new List<Control>();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button seatBtn && seatBtn.Tag != null && seatBtn.Tag.ToString().StartsWith("seat_"))
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

        private void SeatBtn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var seat = btn.Tag as AirportLibrary.model.Seat;
            if (seat == null) return;

            seat.isTaken = !seat.isTaken;
            btn.BackColor = seat.isTaken ? Color.Red : Color.LightGreen;

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Seat SET isTaken=@taken WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", seat.Id);
            cmd.Parameters.AddWithValue("@taken", seat.isTaken ? 1 : 0);
            cmd.ExecuteNonQuery();
            }
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {

        }
    }
}
