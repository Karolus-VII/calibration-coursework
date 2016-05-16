using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Calibration_Interface
{
    public partial class Calibration_Interface1 : UserControl
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        SqlCommandBuilder combuilder;

        string connectionString1;

        public Calibration_Interface1()
        {
            InitializeComponent();

            connectionString1 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\Visual Studio 2015\Projects\Calibration Interface\Calibration Interface\CustomerDB.mdf;Integrated Security=True";

            PopulateCustomerList();
            populate_rdDataGrid();

            this.BackColor = Color.White;
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            port1.PortName = "COM3";
            port1.BaudRate = 9600;
            port1.Parity = Parity.None;
            port1.StopBits = StopBits.One;
            port1.ReadTimeout = 10000;

            try
            {
                port1.Open();
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }

            port2.PortName = "COM5";
            port2.BaudRate = 9600;
            port2.Parity = Parity.None;
            port2.StopBits = StopBits.One;
            port2.ReadTimeout = 10000;
            port2.Encoding = Encoding.ASCII;
            port2.Handshake = Handshake.None;
            port2.RtsEnable = true;
            port2.ReceivedBytesThreshold = 1;

            try
            {
                port2.Open();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }

            secBox.Text = "0";
        }

        private void insertData()
        {
            string Query1 = "INSERT INTO DataSet(Time, Mass) VALUES (@time, @mass)";
                        
            using (connection = new SqlConnection(connectionString1))
            using (command = new SqlCommand(Query1, connection))
            {
                connection.Open();

                command.Parameters.Add("@time", SqlDbType.NVarChar, 50).Value = secBox.Text;
                command.Parameters.Add("@mass", SqlDbType.NVarChar, 50).Value = massBox.Text;

                command.ExecuteNonQuery();
            }

            Calculations();        
        }

        private void port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Invoke(new EventHandler(sensorTimer_Tick));
        }

        private void port2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Invoke(new EventHandler(massTimer_Tick));
        }
        private void populate_rdDataGrid()
        {
            string Query2 = "SELECT * FROM DataSet";

            using (connection = new SqlConnection(connectionString1))
            using (adapter = new SqlDataAdapter(Query2, connection))
            using (combuilder = new SqlCommandBuilder(adapter))
            {
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                rdDataGrid.ReadOnly = true;
                rdDataGrid.DataSource = ds.Tables[0];
            }
        }

        private void PopulateCustomerList()
        {
            string Query1 = "SELECT * FROM CustomerInfo";

            using (connection = new SqlConnection(connectionString1))
            using (adapter = new SqlDataAdapter(Query1, connection))
            {
                DataTable customerTable = new DataTable();
                adapter.Fill(customerTable);

                cstBox1.DisplayMember = "Customer_Name";
                cstBox1.ValueMember = "Id";
                cstBox1.DataSource = customerTable;

            }
        }

        private void PopulateTextBoxes()
        {
            string Query3 = "SELECT * FROM CustomerInfo WHERE [Customer_Name]='" + cstBox1.Text + "' ";
            using (connection = new SqlConnection(connectionString1))
            {
                command = new SqlCommand(Query3, connection);
                connection.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    nmeBox1.Text = (reader["Customer_Name"].ToString());
                    addrsBox1.Text = (reader["Address_1"].ToString());
                    addrsBox2.Text = (reader["Address_2"].ToString());
                    addrsBox3.Text = (reader["Address_3"].ToString());                    
                    phneBox1.Text = (reader["Phone_Number_1"].ToString());                    
                    fxBox1.Text = (reader["Fax_Number"].ToString());
                    emailBox1.Text = (reader["Email_Address"].ToString());
                    wbBox1.Text = (reader["Website"].ToString());

                }
                reader.Close();
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {

            imassBox.Text = massBox.Text;

            Invoke(new EventHandler(dataTimer_Tick));

            dataTimer.Enabled = true;
            dataTimer.Start();
        }
        
        private void stopBtn_Click(object sender, EventArgs e)
        {
            dataTimer.Stop();
            timer1.Stop();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            string Query4 = "TRUNCATE TABLE DataSet";

            using (connection = new SqlConnection(connectionString1))
            {
                command = new SqlCommand(Query4, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
            connection.Close();

            populate_rdDataGrid();
        }

        private void snsrBtn_Click(object sender, EventArgs e)
        {
            sensorTimer.Start();
        }

        private void uncrtBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void cstBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PopulateTextBoxes();
        }

        private void massTimer_Tick(object sender, EventArgs e)
        {
            massTimer.Interval = 1000;
            var massData = port2.ReadLine();

            try
            {
                if (!string.IsNullOrEmpty(massData))
                {
                    massBox.Text = massData.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sensorTimer_Tick(object sender, EventArgs e)
        {
            sensorTimer.Interval = 10000;
            port1.WriteLine("S");

            String sensorData = port1.ReadLine().ToString();
            String[] sensorTempHumid = sensorData.Split(',');

            var temp1 = sensorTempHumid[0];
            var temp2 = sensorTempHumid[1];
            var temp3 = sensorTempHumid[2];
            var temp4 = sensorTempHumid[3];
            var humid1 = sensorTempHumid[4];
            var humid2 = sensorTempHumid[5];

            try
            {
                if (port1.IsOpen)
                {
                    tempBox1.Text = temp1.ToString();
                    tempBox2.Text = temp2.ToString();
                    tempairBox1.Text = temp3.ToString();
                    tempairBox2.Text = temp4.ToString();
                    humidBox1.Text = humid1.ToString();
                    humidBox2.Text = humid2.ToString();
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                throw new ArgumentOutOfRangeException(ex.Message);
            }
        }

        int mil = 0;
        private void dataTimer_Tick(object sender, EventArgs e)
        {
            dataTimer.Interval = 1000;

            secBox.Text = mil.ToString();
            mil++;

            insertData();
            populate_rdDataGrid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = 100;

        }

        private void Calculations()
        {
            //  Water Density Equation  //
            //                          //
            int l = 1;                  //
            int m = 2;                  //  Integer Power Values
            int n = 3;                  //

            decimal a1 = -3.983035m;                             //
            decimal a2 = 301.797m;                               //
            decimal a3 = 522528.9m;                              //
            decimal a4 = 69.34881m;                              //
            decimal a5 = 0.999974950m;                           //
            decimal tempLiquid1 = Decimal.Parse(tempBox1.Text);  //

            decimal elm1 = (tempLiquid1 + a1) * (tempLiquid1 + a1);
            decimal elm2 = tempLiquid1 + a2;
            decimal elm3 = tempLiquid1 + a4;

            decimal density_l = a5 * (1 - ((elm1 * elm2) / (a3 * elm3)));

            //  Air Density Equation  //
            //                        //
            decimal k1 = 3.4844m * (10 * 10 * 10 * 10);
            decimal k2 = -2.52m * (1 / (10 * 10 * 10 * 10 * 10 * 10));
            decimal k3 = 2.0582m * (1 / (10 * 10 * 10 * 10 * 10));
            decimal pA = 1000;
            decimal humidityAmb1 = Decimal.Parse(humidBox1.Text) * 0.01m;
            decimal tempAmb1 = Decimal.Parse(tempairBox1.Text);

            decimal elm5 = k1 * pA;
            decimal elm6 = humidityAmb1 * ((k2 * tempAmb1) + k3);

            decimal density_a = (elm5 + elm6) / (tempAmb1 + 273.15m);

            //  Volume Calculation  //
            //                      //
            decimal imass = Decimal.Parse(imassBox.Text);
            decimal fmass = Decimal.Parse(massBox.Text);
            decimal therm = Decimal.Parse(thermBox.Text);
            decimal templiquid2 = 20;

            decimal Mass = fmass - imass;
            decimal elm7 = 1 / (density_l - density_a);
            decimal elm8 = 1 - (density_a / 8);
            decimal elm9 = 1 - therm * (tempLiquid1 - templiquid2);

            decimal Vol = Mass * elm7 * elm8 * elm9;

            string Query5 = "UPDATE DataSet SET Mass = @mass, Volume = @vol WHERE Mass = @mass";
            
            using (connection = new SqlConnection(connectionString1))
            using (command = new SqlCommand(Query5, connection))

            {
                connection.Open();

                command.Parameters.Add("@mass", SqlDbType.NVarChar, 50).Value = massBox.Text;
                command.Parameters.Add("@vol", SqlDbType.Decimal, 18).Value = Vol;

                command.ExecuteNonQuery();
            }

            //  Flow Rate Calculation  //
            //                         //
            decimal time = Decimal.Parse(secBox.Text);

            if (time != 0)
            {
                decimal flow = Vol / time;

                string Query6 = "UPDATE DataSet SET Mass = @mass, Actual_Flow_Rate = @aflow WHERE Mass = @mass";

                using (connection = new SqlConnection(connectionString1))
                using (command = new SqlCommand(Query6, connection))
                {
                    connection.Open();
                    command.Parameters.Add("@mass", SqlDbType.NVarChar, 50).Value = massBox.Text;
                    command.Parameters.Add("@aflow", SqlDbType.Decimal, 18).Value = flow;
                    command.ExecuteNonQuery();
                }
            }            
        }

        private void prntBtn_Click(object sender, EventArgs e)
        {
            PrintDocument print= new PrintDocument();
            print.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1170);
            print.PrintPage += new PrintPageEventHandler(Print_Page);
            print.Print();

        }

        private void Print_Page (object sender, PrintPageEventArgs e)
        {
            int t = 0;
            int i = 0;
            int width = rdDataGrid.Columns[0].Width;
            int height = rdDataGrid.Rows[0].Height;

            if (t < 2)
            {
                e.Graphics.DrawString("Medical Calibration Systems Ltd.", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, 25, 100);
                e.Graphics.DrawString("[Company Address Here]", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 25, 130);
                e.Graphics.DrawString("[Phone Number Here]", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 25, 155);
                e.Graphics.DrawString("[Fax Number Here]", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 170, 155);
                e.Graphics.DrawString("[Email Here]", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 25, 175);
                e.Graphics.DrawLine(Pens.Black, 20, 200, 807, 200);

                e.Graphics.DrawString("Customer Name: " + nmeBox1.Text.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 25, 210);
                e.Graphics.DrawString("Address: [" + addrsBox1.Text.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 25, 230);
                e.Graphics.DrawString("[" + addrsBox2.Text.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 75, 250);
                e.Graphics.DrawString("[" + addrsBox3.Text.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 75, 270);
                e.Graphics.DrawString("Phone Number: " + phneBox1.Text.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 25, 290);
                e.Graphics.DrawString("Email: " + emailBox1.Text.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 25, 310);

                while (i < rdDataGrid.Rows.Count)
                {
                    int rowheight = rdDataGrid.Rows[i].Height;
                    int colwidth = rdDataGrid.Columns[0].Width;

                    if (height > e.MarginBounds.Height)
                    {
                        height = 100;
                        width = 100;
                        e.HasMorePages = true;

                        return;
                    }

                    height += rowheight;

                    e.Graphics.DrawRectangle(Pens.Black, 20, 230 + height, colwidth, rowheight);

                    e.Graphics.DrawString(rdDataGrid.Rows[i].Cells[0].FormattedValue.ToString(),
                    rdDataGrid.Font, Brushes.Black, new RectangleF(20, 230 + height, colwidth, rowheight));

                    e.Graphics.DrawRectangle(Pens.Black, 20 + colwidth, height, colwidth, rowheight);

                    e.Graphics.DrawString(rdDataGrid.Rows[i].Cells[1].Value.ToString(),
                        rdDataGrid.Font, Brushes.Black, new RectangleF(20 + colwidth,
                        230 + height, width, rowheight));
                    width += colwidth;
                    i++;
                }                    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }
    }
}
