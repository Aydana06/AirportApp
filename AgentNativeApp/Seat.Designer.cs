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
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            label3 = new Label();
            btnSeat = new Button();
            btnChangeStatus = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(157, 34);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "MNA101";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(257, 34);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 1;
            button2.Text = "MNA102";
            button2.UseVisualStyleBackColor = true;
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
            btnSeat.Location = new Point(301, 289);
            btnSeat.Name = "btnSeat";
            btnSeat.Size = new Size(134, 29);
            btnSeat.TabIndex = 7;
            btnSeat.Text = "Суудал оноох";
            btnSeat.UseVisualStyleBackColor = true;
            // 
            // btnChangeStatus
            // 
            btnChangeStatus.Location = new Point(441, 289);
            btnChangeStatus.Name = "btnChangeStatus";
            btnChangeStatus.Size = new Size(227, 29);
            btnChangeStatus.TabIndex = 8;
            btnChangeStatus.Text = "Нислэгийн төлөв өөрчлөх";
            btnChangeStatus.UseVisualStyleBackColor = true;
            btnChangeStatus.Click += btnChangeStatus_Click;
            // 
            // Seat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 413);
            Controls.Add(btnChangeStatus);
            Controls.Add(btnSeat);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Seat";
            Text = "Seat";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label2;
        private Label label3;
        private Button btnSeat;
        private Button btnChangeStatus;
    }
}