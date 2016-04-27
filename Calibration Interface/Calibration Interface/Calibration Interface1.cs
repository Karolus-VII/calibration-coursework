using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

        //SerialPort port1 = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        //SerialPort port2 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);


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

            port2.PortName = "COM7";
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

        }

        private void insertData()
        {
            string Query2 = "INSERT INTO DataSet(Mass, Nominal_Flow_Rate, Actual_Flow_Rate, Uncertainty) VALUES (@mass, @nflow, @aflow, @uncr)";
                        
            using (connection = new SqlConnection(connectionString1))
            using (command = new SqlCommand(Query2, connection))
            {
                connection.Open();

                command.Parameters.Add("@mass", SqlDbType.NVarChar, 50).Value = massBox.Text;                
                command.Parameters.Add("@time", SqlDbType.NVarChar, 50).Value = secBox.Text;
                command.Parameters.Add("@nflow", SqlDbType.NVarChar, 50).Value = nflowBox.Text;
                command.Parameters.Add("@aflow", SqlDbType.NVarChar, 50).Value = aflowBox.Text;
                command.Parameters.Add("@uncr", SqlDbType.NVarChar, 50).Value = uncrBox.Text;
            
                command.ExecuteNonQuery();
            }
                        
        }

        private void captureSensorData(object s, EventArgs e)
        {
            //String sensorData = port1.ReadLine().ToString();
            //String[] sensorTempHumid = sensorData.Split(',');

            //var temp1 = sensorTempHumid[0];
            //var temp2 = sensorTempHumid[1];
            //var temp3 = sensorTempHumid[2];
            //var humid1 = sensorTempHumid[3];
            //var humid2 = sensorTempHumid[4];

            //try
            //{
            //    if (port1.IsOpen)
            //    {
            //        tempBox1.Text = temp1.ToString();
            //        tempairBox1.Text = temp2.ToString();
            //        tempairBox2.Text = temp3.ToString();
            //        humidBox1.Text = humid1.ToString();
            //        humidBox2.Text = humid2.ToString();
            //    }
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    throw new ArgumentOutOfRangeException(ex.Message);
            //}
        }

        private void captureMassData(object s, EventArgs e)
        {
            //var massData = port2.ReadLine();
            //try
            //{
            //    if (!string.IsNullOrEmpty(massData))
            //    {
            //        massBox.Text = massData.ToString();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Invoke(new EventHandler(captureSensorData));
            Invoke(new EventHandler(sensorTimer_Tick));
        }

        private void port2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Invoke(new EventHandler(captureMassData));
            Invoke(new EventHandler(massTimer_Tick));
        }
        private void populate_rdDataGrid()
        {
            string Query3 = "SELECT * FROM DataSet";

            using (connection = new SqlConnection(connectionString1))
            using (adapter = new SqlDataAdapter(Query3, connection))
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
            string Query2 = "SELECT * FROM CustomerInfo WHERE [Customer_Name]='" + cstBox1.Text + "' ";
            using (connection = new SqlConnection(connectionString1))
            {
                command = new SqlCommand(Query2, connection);
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
            Invoke(new EventHandler(dataTimer_Tick));
            dataTimer.Start();
        }
        
        private void stopBtn_Click(object sender, EventArgs e)
        {
            dataTimer.Stop();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            string Query3 = "TRUNCATE TABLE DataSet";

            using (connection = new SqlConnection(connectionString1))
            {
                command = new SqlCommand(Query3, connection);
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
            var humid1 = sensorTempHumid[3];
            var humid2 = sensorTempHumid[4];

            try
            {
                if (port1.IsOpen)
                {
                    tempBox1.Text = temp1.ToString();
                    tempairBox1.Text = temp2.ToString();
                    tempairBox2.Text = temp3.ToString();
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

        private void dataTimer_Tick(object sender, EventArgs e)
        {
            dataTimer.Interval = 1000;
            
            insertData();
            populate_rdDataGrid();
        }
    }
}
