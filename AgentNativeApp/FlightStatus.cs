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
    public partial class FlightStatus : Form
    {
        public FlightStatus()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Seat seat = new Seat();
            seat.ShowDialog();
        }

        private void Passenger_Load(object sender, EventArgs e)
        {

        }
    }
}
