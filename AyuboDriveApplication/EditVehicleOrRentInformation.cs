using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AyuboDriveApplication
{
    public partial class EditVehicleInformation : Form
    {
        public EditVehicleInformation()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login lp = new login();
            lp.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Show();
            this.Hide();
        }

        private void EditVehicleInformation_Load(object sender, EventArgs e)
        {

        }
    }
}
