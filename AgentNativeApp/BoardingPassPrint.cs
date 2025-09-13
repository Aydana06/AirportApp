using System;
using System.Drawing;
using System.Drawing.Printing;
using QRCoder;

namespace Airport.CheckUI
{
    public class BoardingPassPrint
    {
        private string passportNo = "";
        private string seatNo = "";
        private string flightCode = "";
        private string Name = "";

        public BoardingPassPrint(string passport, string seat, string flight, string passengerName)
        {
            passportNo = passport;
            seatNo = seat;
            flightCode = flight;
            Name = passengerName;
        }

        public void Print()
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;

            PrintDialog dlg = new PrintDialog();
            dlg.Document = doc;

            if (dlg.ShowDialog() == DialogResult.OK)
                doc.Print();
        }

        public void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int margin = 20;
            int width = 600;
            int height = 200;
            Rectangle bpRect = new Rectangle(margin, margin, width, height);
            g.FillRectangle(Brushes.White, bpRect);
            g.DrawRectangle(Pens.Black, bpRect);

            float x = margin + 20;
            float y = margin + 20;

            Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            g.DrawString("BOARDING PASS", titleFont, Brushes.DarkBlue, x, y);
            y += 40;

            Font font = new Font("Segoe UI", 12, FontStyle.Regular);

            g.DrawString($"Пасспорт: {passportNo}", font, Brushes.Black, x, y); y += 30;
            g.DrawString($"Нислэгийн код: {flightCode}", font, Brushes.Black, x, y); y += 30;
            g.DrawString($"Суудал: {seatNo}", font, Brushes.Black, x, y); y += 30;
            g.DrawString($"Зорчигчийн нэр: {Name}", font, Brushes.Black, x, y); y += 30;

            // QR код үүсгэх
            string qrData = $"Passport:{passportNo};Flight:{flightCode};Seat:{seatNo}";
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (Bitmap qrBitmap = qrCode.GetGraphic(5))
                {
                    g.DrawImage(qrBitmap, x + 400, margin + 20, 150, 150);
                }
            }

            // Хэвлэсэн огноо
            g.DrawString($"Хэвлэсэн огноо: {DateTime.Now}", new Font("Segoe UI", 10), Brushes.Gray, x, margin + height - 25);
        }
    }
}
