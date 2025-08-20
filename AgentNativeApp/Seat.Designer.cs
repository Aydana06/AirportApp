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
            label3 = new Label();
            btnSeat = new Button();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 89);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 4;
            label2.Text = "Суудалууд:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 34);
            label3.Name = "label3";
            label3.Size = new Size(82, 20);
            label3.TabIndex = 5;
            label3.Text = "Нислэгүүд:";
            // 
            // btnSeat
            // 
            btnSeat.Location = new Point(551, 431);
            btnSeat.Name = "btnSeat";
            btnSeat.Size = new Size(134, 29);
            btnSeat.TabIndex = 7;
            btnSeat.Text = "Суудал оноох";
            btnSeat.UseVisualStyleBackColor = true;
            // 
            // Seat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(713, 472);
            Controls.Add(btnSeat);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "Seat";
            Text = "Seat";
            Load += Seat_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Button btnSeat;
    }
}