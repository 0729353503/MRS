﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;
//using System.Threading.Tasks;

namespace Municipality_Residential_System
{

    public partial class fmLogin : Form
    {
        public fmLogin()
        {
            InitializeComponent();
        }

        //load server (instance names) that are on the computer onto cbbServer when the form opens
        private void fmLogin_Load(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            DataTable dt = SmoApplication.EnumAvailableSqlServers(false);
            if (dt.Rows.Count > 0)
            {
                cbbServer.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    cbbServer.Items.Add(
                         row["Server"] + "\\" + row["Instance"]);
                }
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;

        }
        //populate the cbbDb combobox with database names

        private void cbbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Server sr = new Server(cbbServer.Text);
            try
            {
                foreach (Database db in sr.Databases)
                {
                    cbbDb.Items.Add(db.Name);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /*
        //show user and password textboxes if SQL authentication is checked
        private void rbtSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSQL.Checked == true)
            {
                txtUser.Visible = true;
                txtPassword.Visible = true;
                lblUser.Visible = true;
                lblPassword.Visible = true;
            }
            else
            {
                txtUser.Visible = false;
                txtPassword.Visible = false;
                txtUser.Text = "";
                txtPassword.Text = "";
                lblUser.Visible = false;
                lblPassword.Visible = false;
            }
        }
        */

        //return a connection string based on Design from the dialog
        private string sqlCntStr()
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();
            sql.DataSource = cbbServer.Text;
            sql.InitialCatalog = cbbDb.Text;
            sql.IntegratedSecurity = false;//TRUE
            sql.UserID = txtUser.Text;
            sql.Password = txtPassword.Text;
            return sql.ConnectionString;
        }

        private bool tryConnection()
        {
            bool result = false;
            using (SqlConnection sqlcnt = new SqlConnection())
            {
                sqlcnt.ConnectionString = sqlCntStr();
                try
                {
                    sqlcnt.Open();
                    if (sqlcnt.State == ConnectionState.Open)
                    {
                        result = true;
                    }
                    else
                        result = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

            }
            return result;
        }
        //cancel connection
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //try connection and report result through a message box
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (tryConnection() == true)
            {
                MessageBox.Show("Connection successful", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOK.Focus();
            }
            else

                MessageBox.Show("Connection failed!", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tryConnection() == true)
            {
                Properties.Settings.Default.connection = sqlCntStr();
                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();



            }
            else
                MessageBox.Show("Connection failed!", "Connect Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void rbtSQL_Click(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                btnTest.Focus();

            }
        }

       










    }
}
