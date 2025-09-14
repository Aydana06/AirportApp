using Airport.CheckUI;
using Airport.services;
using AirportLibrary.model;
using AirportLibrary.services;
using System.Data;
using Button = System.Windows.Forms.Button;

namespace AgentNativeApp
{
    public partial class Seat : Form
    {
        private readonly PassengerDto _passenger;
        private readonly SeatService _seatService;
        private readonly FlightService _flightService;
        private List<AirportLibrary.model.Seat> seats = new();
        private Flight currentFlight = null;
        private AirportLibrary.model.Seat? selectedSeat = null;

        private Button? lastSelectedBtn = null;
        private string? lastSelectedBtnText = null;
        private Color lastSelectedBtnColor;

        public Seat(PassengerDto passenger, SeatService seatService, FlightService flightService)
        {
            _passenger = passenger;
            _seatService = seatService;
            _flightService = flightService;

            InitializeComponent();

            SeatPanel.AutoScroll = true;
        }

        private void label1_Click(object sender, EventArgs e)
        { }
        private void button1_Click(object sender, EventArgs e)
        { }
        private void btnChangeStatus_Click(object sender, EventArgs e)
        { }
        private void button2_Click(object sender, EventArgs e)
        { }

        private async void Seat_Load(object sender, EventArgs e)
        {
            lblPassport.Text = _passenger.PassportNo;

            if (!string.IsNullOrWhiteSpace(_passenger.SeatNo))
            {
                var bpPrinter = new BoardingPassPrint(
           _passenger.PassportNo,
           _passenger.SeatNo,
           currentFlight?.FlightCode ?? "Unknown",
           _passenger.FullName
       );
                bpPrinter.Print();
                MessageBox.Show($"Танд {_passenger.SeatNo} суудал аль хэдийн оноогдсон байна!");
                Close();
                return;
            }

            ClearSeats();

            var flight = _flightService.GetFlightById(_passenger.FlightId);
            if (flight != null)
            {
                currentFlight = flight;
                lblFlightCode.Text = $"Нислэг: {flight.FlightCode}";
                await LoadSeats(flight.Id);
            }
        }

        // API-аас Seats авах
        private async Task LoadSeats(int flightId)
        {
            try
            {
                seats = _seatService.GetAllSeats(flightId);
                GenerateSeats();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Суудал татахад алдаа: {ex.Message}");
                seats = new List<AirportLibrary.model.Seat>();
                ClearSeats();
            }
        }

        private void ClearSeats()
        {
            var seatButtons = SeatPanel.Controls.OfType<Button>()
            .Where(c => c.Tag is AirportLibrary.model.Seat)
            .ToList();


            foreach (var ctrl in seatButtons)
            {
                SeatPanel.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            lastSelectedBtn = null;
            selectedSeat = null;
            btnSeat.Enabled = false;
        }

        private void GenerateSeats()
        {
            SeatPanel.SuspendLayout();

            SeatPanel.BackColor = Color.LightGray;
            SeatPanel.BringToFront();
            try
            {
                int startX = 10;
                int startY = 10;
                int btnWidth = 60;
                int btnHeight = 40;
                int marginX = 8;
                int marginY = 8;

                //1A => 1-р эгнээ
                var seatGroups = seats
                .Where(s => !string.IsNullOrWhiteSpace(s.SeatNo))
                .GroupBy(s => GetRowNumber(s.SeatNo))
                .OrderBy(g => g.Key)
                .ToList();


                int currentRow = 0;
                foreach (var rowGroup in seatGroups)
                {
                    var orderedRow = rowGroup.OrderBy(s => s.SeatNo).ToList();
                    int currentCol = 0;


                    foreach (var seat in orderedRow)
                    {
                        var seatBtn = new Button();
                        seatBtn.Text = seat.SeatNo;
                        seatBtn.Width = btnWidth;
                        seatBtn.Height = btnHeight;
                        seatBtn.Tag = seat;

                        if (seat.isTaken)
                        {
                            seatBtn.BackColor = Color.LightCoral;
                            seatBtn.Enabled = false;
                        }
                        else
                        {
                            seatBtn.BackColor = Color.LightGreen;
                            seatBtn.Enabled = true;
                        }

                        seatBtn.Left = startX + (currentRow * (btnWidth + marginX));
                        seatBtn.Top = startY + (currentCol * (btnHeight + marginY));

                        seatBtn.Click += SeatBtn_Click;
                        SeatPanel.Controls.Add(seatBtn);

                        currentCol++;
                    }
                    currentRow++;
                }

                // Зөвхөн зорчигч суудалгүй тохиолдолд товчийг идэвхжүүлнэ
                btnSeat.Enabled = string.IsNullOrWhiteSpace(_passenger.SeatNo);
            }
            finally
            {
                SeatPanel.ResumeLayout();
            }
        }

        // Суудлын дугаараас эгнээний дугаар авах
        private int GetRowNumber(string seatNo)
        {
            if (string.IsNullOrWhiteSpace(seatNo)) return 0;
            int i = 0;
            while (i < seatNo.Length && char.IsDigit(seatNo[i])) i++;


            var rowPart = seatNo.Substring(0, i);
            if (int.TryParse(rowPart, out int row)) return row;
            return 0;
        }

        private async void SeatBtn_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            var seat = btn.Tag as AirportLibrary.model.Seat;
            if (seat == null) return;


            if (seat.isTaken)
            {
                MessageBox.Show($"{seat.SeatNo} суудал аль хэдийн эзлэгдсэн байна!");
                return;
            }

            if (lastSelectedBtn != null && !lastSelectedBtn.IsDisposed)
            {
                lastSelectedBtn.BackColor = lastSelectedBtnColor;
                lastSelectedBtn.Text = lastSelectedBtnText ?? lastSelectedBtn.Text;
            }

            lastSelectedBtn = btn;
            lastSelectedBtnColor = btn.BackColor;
            lastSelectedBtnText = btn.Text;

            btn.BackColor = Color.Gold;
            btn.Text = $"✓ {seat.SeatNo}";

            selectedSeat = seat;
            btnSeat.Enabled = true;

            MessageBox.Show($"Та {seat.SeatNo} суудлыг сонголоо. 'Суудал оноох' товчийг дарна уу.");
        }

        private async void btnSeat_Click_1(object sender, EventArgs e)
        {
            if (selectedSeat == null || currentFlight == null)
            {
                MessageBox.Show("Эхлээд суудал болон нислэгийг сонгоно уу.");
                return;
            }

            try
            {
                bool success = _seatService.AssignSeat(_passenger.Id, selectedSeat.SeatNo, currentFlight.Id);
                if (success)
                {
                    selectedSeat.isTaken = true;
                    _passenger.SeatNo = selectedSeat.SeatNo;

                    MessageBox.Show($"Суудал {selectedSeat.SeatNo} амжилттай оноогдлоо!");

                    var bpPrinter = new BoardingPassPrint(
                        _passenger.PassportNo,
                        selectedSeat.SeatNo,
                        currentFlight.FlightCode,
                        _passenger.FullName
                    );
                    bpPrinter.Print();

                    await LoadSeats(currentFlight.Id);
                    btnSeat.Enabled = false;
                    Close();
                }
                else
                {
                    MessageBox.Show($"Суудал {selectedSeat.SeatNo} аль хэдийн эзлэгдсэн байна!");
                    LoadSeats(currentFlight.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Суудал оноох үед алдаа гарлаа: {ex.Message}");
            }
        }

        private void SeatPanel_Paint(object sender, PaintEventArgs e)
        { }
    }
}
