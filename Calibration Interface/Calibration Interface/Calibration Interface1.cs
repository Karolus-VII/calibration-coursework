using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
        string connectionString1;

        int mil = 0;
        int sec = 0;

        public Calibration_Interface1()
        {
            InitializeComponent();

            connectionString1 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\Visual Studio 2015\Projects\Calibration Interface\Calibration Interface\CustomerDB.mdf;Integrated Security=True";

            this.BackColor = Color.White;
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            //createDataTable();
                                       
        }

        private void dataTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime.Text = (sec + ":" + mil).ToString();

            mil++;

            if (mil >= 1000)
            {
                sec++;
                mil = 0;
            }
            else
            {
                mil++;
            }
        }

        private void createDataTable()
        {
            string Query1 = "CREATE TABLE DataSet(Mass decimal(18,0), Time nvarchar(50), Flow_Rate decimal(18,0))";

            using (connection = new SqlConnection(connectionString1))
            {
                try
                {
                    connection.Open();

                    using (command = new SqlCommand(Query1, connection))
                    {
                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void insertData()
        {
            string Query2 = "INSERT INTO DataSet(Time) VALUES (@time)";

            using (connection = new SqlConnection(connectionString1))
            using (command = new SqlCommand(Query2, connection))
            {
                connection.Open();

                command.Parameters.Add("@time", SqlDbType.NVarChar, 50).Value = elapsedTime.Text;

                command.ExecuteNonQuery();

            }
        }

        private void populate_dtList()
        {
            string Query3 = "SELECT * FROM DataSet";

            using (connection = new SqlConnection(connectionString1))
            using (adapter = new SqlDataAdapter(Query3, connection))
            {
                DataTable readingTable = new DataTable();
                adapter.Fill(readingTable);

                dtList.DisplayMember = "Time";
                dtList.ValueMember = "Id";
                dtList.DataSource = readingTable;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //serialPort1.Open();
            //try
            //{
            //    if (serialPort1.IsOpen)
            //    {
            //        //get Temperature from serial port sent by Arduino
            //        serialPort1.WriteLine("T");
            //        int temperature = (int)(Math.Round(Convert.ToDecimal(serialPort1.ReadLine()), 0));
            //        textBox5.Text = temperature.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //serialPort1.Close();

            dataTimer.Start();

            //insertData();
            //populate_dtList();
            
            if (sec >= 120)
            {
                dataTimer.Stop();
            }                  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataTimer.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Print_Report prntFm = new Print_Report();
            prntFm.Show();

        }
    }
}
