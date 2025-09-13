namespace AgentNativeApp
{
    partial class Seat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            btnSeat = new Button();
            label1 = new Label();
            lblPassport = new Label();
            SeatPanel = new Panel();
            lblFlightCode = new Label();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(48, 125);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 4;
            label2.Text = "Суудалууд:";
            // 
            // btnSeat
            // 
            btnSeat.Location = new Point(567, 416);
            btnSeat.Name = "btnSeat";
            btnSeat.Size = new Size(134, 29);
            btnSeat.TabIndex = 7;
            btnSeat.Text = "Суудал оноох";
            btnSeat.UseVisualStyleBackColor = true;
            btnSeat.Click += btnSeat_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 26);
            label1.Name = "label1";
            label1.Size = new Size(148, 20);
            label1.TabIndex = 8;
            label1.Text = "Пасспортын дугаар:";
            // 
            // lblPassport
            // 
            lblPassport.AutoSize = true;
            lblPassport.Location = new Point(225, 26);
            lblPassport.Name = "lblPassport";
            lblPassport.Size = new Size(0, 20);
            lblPassport.TabIndex = 9;
            // 
            // SeatPanel
            // 
            SeatPanel.Location = new Point(48, 171);
            SeatPanel.Name = "SeatPanel";
            SeatPanel.Size = new Size(653, 222);
            SeatPanel.TabIndex = 10;
            // 
            // lblFlightCode
            // 
            lblFlightCode.AutoSize = true;
            lblFlightCode.Location = new Point(48, 76);
            lblFlightCode.Name = "lblFlightCode";
            lblFlightCode.Size = new Size(0, 20);
            lblFlightCode.TabIndex = 12;
            // 
            // Seat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(713, 457);
            Controls.Add(lblFlightCode);
            Controls.Add(SeatPanel);
            Controls.Add(lblPassport);
            Controls.Add(label1);
            Controls.Add(btnSeat);
            Controls.Add(label2);
            Name = "Seat";
            Text = "Seat";
            Load += Seat_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Button btnSeat;
        private Label label1;
        private Label lblPassport;
        private Panel SeatPanel;
        private Label lblFlightCode;
    }
}