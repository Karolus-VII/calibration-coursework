using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Calibration_Interface
{
    public partial class Customer_Form : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        string connectionString1;

        public Customer_Form()
        {
            InitializeComponent();

            connectionString1 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Documents\Visual Studio 2015\Projects\Calibration Interface\Calibration Interface\CustomerDB.mdf;Integrated Security=True";

            PopulateCustomerList();
            PopulateComboBox();

        }

        private void Customer_Form_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(808, 648);
            this.MaximizeBox = false;

        }

        private void PopulateCustomerList()
        {
            string Query1 = "SELECT * FROM CustomerInfo";

            using (connection = new SqlConnection(connectionString1))
            using (adapter = new SqlDataAdapter(Query1, connection))
            {
                DataTable customerTable = new DataTable();
                adapter.Fill(customerTable);

                cstBox.DisplayMember = "Customer_Name";
                cstBox.ValueMember = "Id";
                cstBox.DataSource = customerTable;

            }
        }

        private void PopulateTextBoxes()
        {
            string Query2 = "SELECT * FROM CustomerInfo WHERE [Customer_Name]='" + cstBox.Text + "' ";
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
                    steBox.Text = (reader["State"].ToString());
                    pstBox1.Text = (reader["Postal_Code"].ToString());
                    phneBox1.Text = (reader["Phone_Number_1"].ToString());
                    phneBox2.Text = (reader["Phone_Number_2"].ToString());
                    fxBox1.Text = (reader["Fax_Number"].ToString());
                    emailBox1.Text = (reader["Email_Address"].ToString());
                    wbBox1.Text = (reader["Website"].ToString());

                }
                reader.Close();
            }
        }

        private void PopulateComboBox()
        {
            string Query3 = "SELECT * FROM States";

            using (connection = new SqlConnection(connectionString1))
            using (adapter = new SqlDataAdapter(Query3, connection))
            {
                DataTable customerTable = new DataTable();
                adapter.Fill(customerTable);

                steBox2.DisplayMember = "Name";
                steBox2.ValueMember = "Id";
                steBox2.DataSource = customerTable;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
       
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            PopulateTextBoxes();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string Query3 = "INSERT INTO CustomerInfo (Customer_Name, Address_1, Address_2, Address_3, State, Postal_Code, Phone_Number_1, Phone_Number_2, Fax_Number, Email_Address, Website)" +
                        "VALUES (@Customer_Name, @Address_1, @Address_2, @Address_3,@State, @Postal_Code, @Phone_Number_1, @Phone_Number_2, @Fax_Number, @Email_Address, @Website)";
            
            using (connection = new SqlConnection(connectionString1))
            using (command = new SqlCommand(Query3, connection))
            {
                connection.Open();

                command.Parameters.Add("@Customer_Name", SqlDbType.NVarChar, 100).Value = nmeBox2.Text;
                command.Parameters.Add("@Address_1", SqlDbType.NVarChar, 100).Value = addrsBox4.Text;
                command.Parameters.Add("@Address_2", SqlDbType.NVarChar, 100).Value = addrsBox5.Text;
                command.Parameters.Add("@Address_3", SqlDbType.NVarChar, 100).Value = addrsBox6.Text;
                command.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = steBox2.Text;
                command.Parameters.Add("@Postal_Code", SqlDbType.NVarChar, 50).Value = pstBox2.Text;
                command.Parameters.Add("@Phone_Number_1", SqlDbType.NVarChar, 50).Value = phneBox3.Text;
                command.Parameters.Add("@Phone_Number_2", SqlDbType.NVarChar, 50).Value = phneBox4.Text;
                command.Parameters.Add("@Fax_Number", SqlDbType.NVarChar, 50).Value = fxBox2.Text;
                command.Parameters.Add("@Email_Address", SqlDbType.NVarChar, 50).Value = emailBox2.Text;
                command.Parameters.Add("@Website", SqlDbType.NVarChar, 50).Value = wbBox2.Text;

                command.ExecuteNonQuery();
                
            }

            PopulateCustomerList();
            connection.Close();

            nmeBox2.Text = "";
            addrsBox4.Text = "";
            addrsBox5.Text = "";
            addrsBox6.Text = "";
            steBox2.Text = "";
            pstBox2.Text = "";
            phneBox3.Text = "";
            phneBox4.Text = "";
            fxBox2.Text = "";
            emailBox2.Text = "";
            wbBox2.Text = "";

        }

        private void updtBtn_Click(object sender, EventArgs e)
        {
            string Query4 = "UPDATE CustomerInfo SET Customer_Name = @cust, Address_1 = @add1, Address_2 = @add2, Address_3 = @add3, State = @ste, Postal_Code = @pst, Phone_Number_1 = @phn1, Phone_Number_2 = @phn2, Fax_Number = @fx, Email_Address = @email, Website = @web WHERE Customer_Name = @cust";

            connection = new SqlConnection(connectionString1);
            command = new SqlCommand(Query4, connection);

            try
            {
                connection.Open();

                command.Parameters.Add("@cust", SqlDbType.NVarChar, 100).Value = nmeBox1.Text;
                command.Parameters.Add("@add1", SqlDbType.NVarChar, 100).Value = addrsBox1.Text;
                command.Parameters.Add("@add2", SqlDbType.NVarChar, 100).Value = addrsBox2.Text;
                command.Parameters.Add("@add3", SqlDbType.NVarChar, 100).Value = addrsBox3.Text;
                command.Parameters.Add("@ste", SqlDbType.NVarChar, 50).Value = steBox.Text;
                command.Parameters.Add("@pst", SqlDbType.NVarChar, 50).Value = pstBox1.Text;
                command.Parameters.Add("@phn1", SqlDbType.NVarChar, 50).Value = phneBox1.Text;
                command.Parameters.Add("@phn2", SqlDbType.NVarChar, 50).Value = phneBox2.Text;
                command.Parameters.Add("@fx", SqlDbType.NVarChar, 50).Value = fxBox1.Text;
                command.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = emailBox1.Text;
                command.Parameters.Add("@web", SqlDbType.NVarChar, 50).Value = wbBox1.Text;

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            PopulateCustomerList();
            connection.Close();

            nmeBox2.Text = "";
            addrsBox4.Text = "";
            addrsBox5.Text = "";
            addrsBox6.Text = "";
            steBox2.Text = "";
            pstBox2.Text = "";
            phneBox3.Text = "";
            phneBox4.Text = "";
            fxBox2.Text = "";
            emailBox2.Text = "";
            wbBox2.Text = "";
        }
    }
}
