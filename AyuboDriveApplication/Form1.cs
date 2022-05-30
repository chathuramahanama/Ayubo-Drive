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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string label1 = textBox1.Text;
            string label2 = textBox2.Text;

            if (label1 == "admin" && label2 == "qwerty")
            {
                MessageBox.Show("                                                 LOGIN SUCCESSFUL     |                                                                 CLICK [ OK ]    OR     PRESS  ENTER  BUTTON          ");
                this.Hide(); 
                MainMenu obj = new MainMenu();
                obj.Show();
            }
            else
            {
                MessageBox.Show("                                        LOGIN FAIL!     |      CLICK  [ OK ]                                              PLEASE ENTER YOUR CORRECT USERNAME AND PASSWORD  ");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("                                              |||    PRESS ENTER    |||                                                                   THE PROGRAM CLOSED IN SUCCESSFULLY      ");
            Application.Exit();
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }

}



