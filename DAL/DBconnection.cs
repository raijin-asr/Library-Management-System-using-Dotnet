using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using System.Configuration;

namespace LibMgmt.DAL
{
    public static class DBconnection
    {
        public static SqlConnection connection()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LibMgmtCon"].ConnectionString);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                return con;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static void closeconnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LibMgmtCon"].ConnectionString);
            try
            {
                con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
           
        }

    }
}
