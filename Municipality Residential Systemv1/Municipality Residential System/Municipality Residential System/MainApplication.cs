using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Municipality_Residential_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            fmReport report = new fmReport();
            report.Show();
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            fmRegister register = new fmRegister();
            register.Show();
        }

        private void connectToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmLogin login = new fmLogin();
            login.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUserOfficial_Click(object sender, EventArgs e)
        {
            fmUserofficials register = new fmUserofficials();
            register.Show();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            fmRegister register = new fmRegister();
            register.Show();
        }

        private void btnResident_Click(object sender, EventArgs e)
        {
            fmResident register = new fmResident();
            register.Show();
        }

        private void btnBusiness_Click(object sender, EventArgs e)
        {
            fmBusiness register = new fmBusiness();
            register.Show();
        }

        private void btnReport_Click_1(object sender, EventArgs e)
        {
            fmReport register = new fmReport();
            register.Show();
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            fmProperty register = new fmProperty();
            register.Show();
        }

        private void btnElectricityBill_Click(object sender, EventArgs e)
        {
            fmInvitations register = new fmInvitations();
            register.Show();
        }



    }
}
