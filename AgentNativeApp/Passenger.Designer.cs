namespace AgentNativeApp
{
    partial class Passenger
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
            label1 = new Label();
            label2 = new Label();
            PassportNumber = new TextBox();
            lblAssignedSeat = new Label();
            btnSearch = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(109, 150);
            label1.Name = "label1";
            label1.Size = new Size(148, 20);
            label1.TabIndex = 0;
            label1.Text = "Пасспортын дугаар:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(109, 194);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 1;
            label2.Text = "Оноосон суудал:";
            // 
            // PassportNumber
            // 
            PassportNumber.Location = new Point(275, 150);
            PassportNumber.Name = "PassportNumber";
            PassportNumber.Size = new Size(193, 27);
            PassportNumber.TabIndex = 2;
            PassportNumber.TextChanged += textBox1_TextChanged;
            // 
            // lblAssignedSeat
            // 
            lblAssignedSeat.AutoSize = true;
            lblAssignedSeat.Location = new Point(275, 194);
            lblAssignedSeat.Name = "lblAssignedSeat";
            lblAssignedSeat.Size = new Size(111, 20);
            lblAssignedSeat.TabIndex = 3;
            lblAssignedSeat.Text = "_________________";
            lblAssignedSeat.Click += label3_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(474, 150);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Хайх";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // button2
            // 
            button2.Location = new Point(574, 150);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 5;
            button2.Text = "Бүртгэх";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Passenger
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(767, 354);
            Controls.Add(button2);
            Controls.Add(btnSearch);
            Controls.Add(lblAssignedSeat);
            Controls.Add(PassportNumber);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Passenger";
            Text = "Passenger";
            Load += Passenger_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox PassportNumber;
        private Label lblAssignedSeat;
        private Button btnSearch;
        private Button button2;
    }
}