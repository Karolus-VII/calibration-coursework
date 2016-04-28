using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calibration_Interface
{
    public partial class MeCS : Form
    {
        const int i = 0;
        const int j = i + 1;
        const int k = j + 1;
        const int l = k + 1;

        public MeCS()
        {
            InitializeComponent();
        }

        private void MeCS_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1336, 760);
            this.Text = "MeCS - Medical Calibration System";
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Calibration_Interface1 ctrl1 = new Calibration_Interface1();
            ctrl1.Dock = DockStyle.Fill;
            string title1 = "Pump " + (i + 1).ToString();
            TabPage page1 = new TabPage(title1);
            page1.Controls.Add(ctrl1);
            tabControl1.TabPages.Insert(i, page1);

            Calibration_Interface2 ctrl2 = new Calibration_Interface2();
            ctrl2.Dock = DockStyle.Fill;
            string title2 = "Pump " + (i + 2).ToString();
            TabPage page2 = new TabPage(title2);
            page2.Controls.Add(ctrl2);
            tabControl1.TabPages.Insert(j, page2);

            Calibration_Interface3 ctrl3 = new Calibration_Interface3();
            ctrl3.Dock = DockStyle.Fill;
            string title3 = "Pump " + (i + 3).ToString();
            TabPage page3 = new TabPage(title3);
            page3.Controls.Add(ctrl3);
            tabControl1.TabPages.Insert(k, page3);

            Calibration_Interface4 ctrl4 = new Calibration_Interface4();
            ctrl4.Dock = DockStyle.Fill;
            string title4 = "Pump " + (i + 4).ToString();
            TabPage page4 = new TabPage(title4);
            page4.Controls.Add(ctrl4);
            tabControl1.TabPages.Insert(l, page4);

            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.Gray;

            textBox1.BackColor = Color.LightGray;
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(i);

            button1.BackColor = Color.LightBlue;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.Gray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(j);

            button1.BackColor = Color.Gray;
            button2.BackColor = Color.LightBlue;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.Gray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(k);

            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.LightBlue;
            button4.BackColor = Color.Gray;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(l);

            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.LightBlue;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Customer_Form cusFrm = new Customer_Form();
            cusFrm.Show();

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            string info1 = "Select pump 1 for calibration.";
            textBox1.Text = info1;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            string info2 = "Select pump 2 for calibration.";
            textBox1.Text = info2;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            string info3 = "Select pump 3 for calibration.";
            textBox1.Text = info3;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            string info4 = "Select pump 4 for calibration.";
            textBox1.Text = info4;
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            string info7 = "View and edit customer information; includes adding new and deleting saved entries.";
            textBox1.Text = info7;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            string info8 = "View information on all jobs performed.";
            textBox1.Text = info8;
        }
        
    }
}
