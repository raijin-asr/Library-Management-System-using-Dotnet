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
    public partial class FirstPage : Form
    {
        public FirstPage()
        {
            InitializeComponent();
            Binddatagrid();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            //select [Name] as Book, [PageCount] as [totalpages] from Book 
        }
        public void Binddatagrid()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = DBconnection.connection())
            {
                string sql = string.Empty;
                sql = "select [Name] as Book, [PageCount] as [totalpages] from Book  ";
                SqlCommand cmd = new SqlCommand(sql.ToString(), con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);
                dg1.AutoGenerateColumns = false;
                dg1.Columns[0].DataPropertyName = "Book";
                dg1.Columns[1].DataPropertyName = "totalpages";
                
                dg1.DataSource = ds.Tables[0];
                //dg1.Columns[3]. = "edit"


            }
        }

        private void dg1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.ColumnIndex == 2)
            //MessageBox.Show("Edit button clicked");
            if (e.ColumnIndex == dg1.Columns["Edit"].Index && e.RowIndex >= 0)
                MessageBox.Show("Edit button clicked");
        }

    }
}
