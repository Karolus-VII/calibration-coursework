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
            
        }

        private void insertData()
        {
            string Query2 = "INSERT INTO DataSet(Mass, Time, Nominal_Flow_Rate, Actual_Flow_Rate, Uncertainty) VALUES (@mass, @time, @nflow, @aflow, @uncr)";
            
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

        private void snsrBtn_Click(object sender, EventArgs e)
        {
            serialPort1.Open();
            try
            {
                if (serialPort1.IsOpen)
                {                    
                    serialPort1.WriteLine("T");
                    int temp1 = (int)(Math.Round(Convert.ToDecimal(serialPort1.ReadLine()), 0));
                    textBox5.Text = temp1.ToString();

                    serialPort1.WriteLine("t");
                    int temp2 = (int)(Math.Round(Convert.ToDecimal(serialPort1.ReadLine()), 0));
                    textBox3.Text = temp2.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            serialPort1.Close();
        }

        private void tbBtn_Click(object sender, EventArgs e)
        {
            insertData();
            populate_rdDataGrid();
        }

        private void uncrtBtn_Click(object sender, EventArgs e)
        {

        }

        private void cstBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PopulateTextBoxes();
        }
    }
}
