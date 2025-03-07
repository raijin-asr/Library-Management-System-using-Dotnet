using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LibMgmt.DAL;

namespace LibMgmt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
            AddBook frm = new AddBook();
            frm.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == null || txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Please enter username.","Validation", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (txtPassword.Text == null || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (SqlConnection con = DBconnection.connection())
                {
                    string message = string.Empty;
                    SqlCommand cmd = new SqlCommand("checklogginuser", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        message = result.ToString();
                    }
                    if (message.ToLower() == "2")
                    {
                        FirstPage frm = new FirstPage();
                        frm.Show();
                        this.Hide();
                    }
                    else if (message.ToLower() == "1")
                    {
                        AdminForm frm = new AdminForm();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username or/ and Password is incorrect.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            FirstPage frm = new FirstPage();
            frm.Show();
            this.Hide();
        }
    }
}
