using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.DTO.Request;

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

        public static DataTable getListUsersExceptRoleAdmin()
        {
            ConnectionDatabase.getConnection();
            string query = @"select * from users
                            where role != 'admin'";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);
            ConnectionDatabase.closeConnection();

            return ds.Tables[0];
        }

        public static DataTable getListUsersExceptCurrentUser(int currentUserId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select * from users
                            where id != @currentUserId
                            and role != 'admin'";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@currentUserId", currentUserId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);
            ConnectionDatabase.closeConnection();

            return ds.Tables[0];
        }

        public static UsersDTO getUserById(int id)
        {
            ConnectionDatabase.getConnection();
            string query = @"select * from users where id = @id";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@id", id);

            IDataReader reader = cmd.ExecuteReader();
            UsersDTO user = null;

            while (reader.Read())
            {
                int userId = Convert.ToInt32(reader["id"]);
                string userName = reader["name"].ToString();
                string userEmail = reader["email"].ToString();
                string userRole = reader["role"].ToString();
                user = new UsersDTO
                {
                    ID = userId,
                    Name = userName,
                    Email = userEmail,
                    Role = userRole
                };
            }

            ConnectionDatabase.closeConnection();

            return user;
        }
        public static void updateUser(UpdateUserDTO user)
        {
            ConnectionDatabase.getConnection();
            string query = @"update users set 
                            name = @name
                            where id = @id";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.ExecuteNonQuery();
            ConnectionDatabase.closeConnection();
        }
        public static bool deleteUser(int id)
        {
            ConnectionDatabase.getConnection();
            string query = @"delete from users where id = @id";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                cmd.ExecuteNonQuery();
            } catch (Exception ex)
            {
                return false;
            }
            ConnectionDatabase.closeConnection();
            return true;
        }

        public static List<int> getListPartnerIdFollowTaskId(int taskId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select userId
                             from usersTasks
                             where taskId = @taskId";

            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            SqlDataReader read = cmd.ExecuteReader();
            List<int> arrPartnerId = new List<int>();
            while(read.HasRows)
            {
                while (read.Read())
                {
                    arrPartnerId.Add(read.GetInt32(0));
                }
                read.NextResult();
            }
            ConnectionDatabase.closeConnection();
            return arrPartnerId;
        }

        public static DataTable getListUserOfTask(int taskId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select *
                            from usersTasks, users
                            where taskId = @taskId 
                                and users.id = usersTasks.userId";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);
            ConnectionDatabase.closeConnection();

            return ds.Tables[0];
        }
    }
}