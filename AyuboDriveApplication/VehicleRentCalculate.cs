using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace AyuboDriveApplication
{
    public partial class VehicleRentCalculate : Form
    {
        public VehicleRentCalculate()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=LAPTOP-PS2GJUT8\\SQLEXPRESS;Initial Catalog=AyuboDriveApplication;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select VehicleNumber from VEHICLE_DEATILS";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            comboBox1.Items.Add(" SELECT VEHICLE NUMBER");

            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["VehicleNumber"].ToString());
            }

            con.Close();
            comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            login lp = new login();
            lp.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Show();
            this.Hide();
        }

        private void VehicleRentCalculate_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex != 0)
            {

                if (!(dateTimePicker2.Value <= dateTimePicker1.Value))
                {

                    string vehicleNo = comboBox1.SelectedItem.ToString();
                    DateTime rentedDate = dateTimePicker1.Value;
                    DateTime returnDate = dateTimePicker2.Value;
                    bool withDriver = false;

                    if (checkBox1.Checked == true)
                    {
                        withDriver = true;
                    }

                    Operation op = new Operation();
                    label3.Text = "Total Rent = " + Convert.ToString(op.calculateVehicleRent(vehicleNo, rentedDate, returnDate, withDriver));
                }

                else
                {
                    MessageBox.Show("Please enter correct date range!");
                }
            }

            else
            {
                MessageBox.Show("Please enter all values!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
            label3.Text = "Total Rent";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
