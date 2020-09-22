using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DAL
{
    public class ConnectionDatabase
    {
        public static SqlConnection conn;
        public static void getConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["todoConnectionString"].ConnectionString;
            conn = new SqlConnection(connString);
            conn.Open();
        }

        public static void closeConnection()
        {
            conn.Close();
        }
    }
}