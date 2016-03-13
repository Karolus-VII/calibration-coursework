using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calibration_Interface
{
    public partial class Calibration_Interface1 : UserControl
    {

        public Calibration_Interface1()
        {
            InitializeComponent();

            this.BackColor = Color.White;
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            //Text Box formatting upon initialization of program
            //this.textBox1.ReadOnly = true;
            //this.textBox2.ReadOnly = true;
            //this.textBox3.ReadOnly = true;
            //this.textBox4.ReadOnly = true;
            //this.textBox5.ReadOnly = true;
            //this.textBox6.ReadOnly = true;

            //this.textBox1.Text = "0";
            //this.textBox2.Text = "0";
            //this.textBox3.Text = "0";
            //this.textBox4.Text = "0";
            //this.textBox5.Text = "0";
            //this.textBox6.Text = "Unavailable";

            //this.textBox1.TextAlign = HorizontalAlignment.Right;
            //this.textBox2.TextAlign = HorizontalAlignment.Right;
            //this.textBox3.TextAlign = HorizontalAlignment.Right;
            //this.textBox4.TextAlign = HorizontalAlignment.Right;
            //this.textBox5.TextAlign = HorizontalAlignment.Right;
            //this.textBox6.TextAlign = HorizontalAlignment.Right;

            //this.textBox1.BackColor = Color.LightGray;
            //this.textBox2.BackColor = Color.LightGray;
            //this.textBox3.BackColor = Color.LightGray;
            //this.textBox4.BackColor = Color.LightGray;
            //this.textBox5.BackColor = Color.LightGray;
            //this.textBox6.BackColor = Color.LightGray;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Print_Report prntFm = new Print_Report();
            prntFm.Show();

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
