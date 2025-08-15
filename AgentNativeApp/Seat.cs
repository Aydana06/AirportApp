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
        {
            FlightStatus flightStatus = new FlightStatus();
            flightStatus.ShowDialog();
        }
    }
}
