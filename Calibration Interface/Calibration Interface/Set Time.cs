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
    public partial class Set_Time : Form
    {
        public Set_Time()
        {
            InitializeComponent();
        }

        private void Set_Time_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(290, 250);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
                        
        }


    }
}
