namespace AgentNativeApp
{
    partial class FlightStatus
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
            btnSave = new Button();
            StatusBox = new ComboBox();
            label2 = new Label();
            FlightCode = new TextBox();
            label3 = new Label();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(406, 195);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 41);
            btnSave.TabIndex = 20;
            btnSave.Text = "Хадгалах";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // StatusBox
            // 
            StatusBox.FormattingEnabled = true;
            StatusBox.Items.AddRange(new object[] { "Бүртгэж байна", "Онгоцонд сууж байна", "Ниссэн", "Хойшилсон", "Цуцалсан" });
            StatusBox.Location = new Point(226, 119);
            StatusBox.Name = "StatusBox";
            StatusBox.Size = new Size(227, 28);
            StatusBox.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(67, 119);
            label2.Name = "label2";
            label2.Size = new Size(131, 20);
            label2.TabIndex = 18;
            label2.Text = "Нислэгийн төлөв:";
            // 
            // FlightCode
            // 
            FlightCode.Location = new Point(226, 72);
            FlightCode.Name = "FlightCode";
            FlightCode.Size = new Size(227, 27);
            FlightCode.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(67, 72);
            label3.Name = "label3";
            label3.Size = new Size(137, 20);
            label3.TabIndex = 16;
            label3.Text = "Нислэгийн дугаар:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(67, 195);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 21;
            // 
            // FlightStatus
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 290);
            Controls.Add(lblStatus);
            Controls.Add(btnSave);
            Controls.Add(StatusBox);
            Controls.Add(label2);
            Controls.Add(FlightCode);
            Controls.Add(label3);
            Name = "FlightStatus";
            Text = "FlightStatus";
            Load += Passenger_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSave;
        private ComboBox StatusBox;
        private Label label2;
        private TextBox FlightCode;
        private Label label3;
        private Label lblStatus;
    }
}