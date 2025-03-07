using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibMgmt.DAL
{
    class sqlhelper
    {
        public DataTable SelectDataByID(int id, string tablename)
        {
            using (SqlConnection con = DBconnection.connection())
            {
                string query = "SELECT * from " + tablename +" where ID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }

        }
        public int InsertUsingSP(string itemName, int ItemID, int Qty, decimal amount, decimal dis, DateTime datentime)
        {
            using (SqlConnection con = DBconnection.connection())
            {
                SqlCommand cmd = new SqlCommand("spInsertPurchase", con);
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@ItemId", ItemID);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@DateTim", datentime);
                cmd.Parameters.AddWithValue("@Discount", dis);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
    }
}
