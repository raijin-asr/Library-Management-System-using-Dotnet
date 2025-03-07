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
using LibMgmt.DAL.dbcontext;

namespace LibMgmt
{
    public partial class AddBook : Form
    {
        int id = 0;
        public AddBook()
        {
            InitializeComponent();
        }

        private void AddBook_Load(object sender, EventArgs e)
        {
            LoadAuthor();
            LoadType();
            LoadGrid();
        }
        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = DBconnection.connection())
            {
                string sql = string.Empty;
                sql = "select b.[Name] as Book, [PageCount] as [total pages], a.FirstName as Writer, t.[Name] as [type] from Book b inner join Author a on b.AuthorId = a.Id ";
                sql += "inner join[type] t on b.TypeId = t.Id";
                SqlCommand cmd = new SqlCommand(sql.ToString(), con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);
                dgbook.AutoGenerateColumns = true;
                dgbook.DataSource = ds.Tables[0];
                //dgbook.DataMember = ds.Tables[0].ToString();
            }
        }
        public void LoadAuthor()
        {
            DataSet ds = new DataSet();
            ds = FillComboBox("author");
            cmbAuthID.DataSource = ds.Tables[0];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cmbAuthID.DisplayMember = "fullName";
                cmbAuthID.ValueMember = "id";
            }
        }
        public void LoadType()
        {
            DataSet ds = new DataSet();
            ds = FillComboBox("Type");
            cmbType.DataSource = ds.Tables[0];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cmbType.DisplayMember = "Name";
                cmbType.ValueMember = "id";
            }
        }
        public DataSet FillComboBox(String tablename)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = DBconnection.connection())
            {
                string sql = string.Empty;
                if (tablename.ToLower() == "author")
                {
                    sql = "select id, (FirstNAME + ' ' + lastName) as fullName from " + tablename;
                }
                else if (tablename.ToLower() == "type")
                {
                    sql = "select id, Name from [" + tablename + "]";
                }
                SqlCommand cmd = new SqlCommand(sql.ToString(), con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);
            }
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Name = txtName.Text;
            string pagecount = txtPagecount.Text;
            int AuthID = Convert.ToInt32(cmbAuthID.SelectedValue);
            int Typeid = Convert.ToInt32(cmbType.SelectedValue);
            if (validation())
            {
                using(DBLibContextDataContext db = new DBLibContextDataContext())
                {
                    string sql = string.Empty;
                    sql = "Insert into book ([Name], [PageCount], AuthorID, TypeID) values('"+Name+"', '"+pagecount+"','" + AuthID + "', '"+Typeid +"')";
                    int i = db.ExecuteCommand(sql);
                    if (i > 0)
                    {
                        MessageBox.Show("Saved successfully");
                        AddBook_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Error occured while saving");
                    }
                }
            }
        }
        public bool validation()
        {
            if (txtName.Text == null || txtName.Text == "")
            {
                MessageBox.Show("Please Enter Name","Validation",MessageBoxButtons.OK);
                return false;
            }
            else if (txtPagecount.Text == null || txtPagecount.Text == "")
            {
                MessageBox.Show("Please Enter PageCount", "Validation", MessageBoxButtons.OK);
                return false;
            }
            else
                return true;
        }
        
       
        private DataTable PopulateDataGridView()
        {
            string sql = string.Empty;
            sql = "select b.[Name] as Book, [PageCount] as [total pages], a.FirstName as Writer, t.[Name] as [type] from Book b inner join Author a on b.AuthorId = a.Id ";
            sql += "inner join[type] t on b.TypeId = t.Id where b.[Name] like '%"+ txtSearch.Text +"%'";
            using (SqlConnection con = DBconnection.connection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                   // cmd.Parameters.AddWithValue("@ContactName", txtName.Text.Trim());
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataSet dt = new DataSet();
                        sda.Fill(dt);
                        return dt.Tables[0];
                    }
                }
            }
        }

        private void txtSearch_KeyUp(object sender, EventArgs e)
        {
            dgbook.DataSource = this.PopulateDataGridView();
        }

        private void dgbook_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtName.Text = dgbook.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPagecount.Text = dgbook.Rows[e.RowIndex].Cells[1].Value.ToString();
            cmbAuthID.Text = dgbook.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
    }
}
