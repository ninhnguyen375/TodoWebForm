using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DTO;

namespace TodoListWebForm.App_Code.DAL
{
    public class TasksDAL
    {
        public static DataTable GetAllTasks()
        {
            ConnectionDatabase.getConnection();
            string query = "select * from tasks";
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = ConnectionDatabase.conn.CreateCommand();
            cmd.CommandText = query;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            ConnectionDatabase.closeConnection();
            return ds.Tables[0];
        }

        public static int CreateTask(TasksDTO task, List<int> arrUser, int ownerId)
        {
            ConnectionDatabase.getConnection();
            string query = @"insert into tasks(title, startDate, endDate, status, private)
                            values(@title, @startDate, @endDate, @status, @private); 
                            select MAX(id) FROM tasks";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@startDate", Convert.ToDateTime(task.startDate));
            cmd.Parameters.AddWithValue("@endDate", Convert.ToDateTime(task.endDate));
            cmd.Parameters.AddWithValue("@status", task.Status);
            cmd.Parameters.AddWithValue("@private", task.Private);
            Int32 lastestId = (Int32)cmd.ExecuteScalar();

            // add partner into task

            for (int i = 0; i < arrUser.Count; i++)
            {
                query = @"insert into usersTasks(userId, taskId, isOwner)
                        values (@userId, @taskId, 0)";
                cmd = new SqlCommand(query, ConnectionDatabase.conn);
                cmd.Parameters.AddWithValue("@userId", arrUser[i]);
                cmd.Parameters.AddWithValue("@taskId", lastestId);
                cmd.ExecuteNonQuery();
            }

            // add the owner into task
            query = @"insert into usersTasks(userId, taskId, isOwner)
                        values (@userId, @taskId, 1)";
            cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@userId", ownerId);
            cmd.Parameters.AddWithValue("@taskId", lastestId);
            cmd.ExecuteNonQuery();

            ConnectionDatabase.closeConnection();
            return 1;
        }

        public static int DeleteTaskById(String id)
        {
            ConnectionDatabase.getConnection();
            string sql = @"delete from tasks 
                            where id = @id";
            SqlCommand cmd = new SqlCommand(sql, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();
            ConnectionDatabase.closeConnection();
            return result;
        }
    }
}