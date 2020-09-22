using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DAL
{
    public class UsersDAL
    {
        public static DataTable getListUsers()
        {
            ConnectionDatabase.getConnection();
            string query = "select * from users";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);
            ConnectionDatabase.closeConnection();

            return ds.Tables[0];
        }
    }
}