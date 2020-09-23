﻿using System;
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

        public static List<TasksDTO> GetAllTasksByUserId(int userId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select * from (
	                                        select * from tasks
	                                        where id in (select taskId 
				                                        from usersTasks
				                                        where userId = @userId)
	                                        union
	                                        select * from tasks
	                                        where id in (select taskId 
				                                        from usersTasks
				                                        where userId != @userId)
				                                        and private = 0
                                        ) as temp
                                        order by startDate asc";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = cmd.ExecuteReader();
            List<TasksDTO> arr = new List<TasksDTO>();
            while(reader.HasRows)
            {
                while(reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string startDate = Convert.ToString(reader.GetDateTime(2));
                    string endDate = Convert.ToString(reader.GetDateTime(3));
                    string status = reader.GetString(4);
                    bool Private = Convert.ToBoolean(reader.GetValue(5));
                    arr.Add(new TasksDTO(id,
                                         title,
                                         startDate,
                                         endDate,
                                         status,
                                         Private));
                }
                reader.NextResult();
            }
            return arr;
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

        public static int DeleteTaskById(string taskId)
        {
            ConnectionDatabase.getConnection();
            
            // remove comment 
            string sql = @"delete from comments 
                    where taskId = @taskId";
            SqlCommand cmd = new SqlCommand(sql, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();

            // remove usersTasks
            sql = @"delete from usersTasks
                            where taskId = @taskId";
            cmd = new SqlCommand(sql, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();

            // remove tasks
            sql = @"delete from tasks 
                    where id = @taskId";
            cmd = new SqlCommand(sql, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.ExecuteNonQuery();
            ConnectionDatabase.closeConnection();
            return 1;
        }

        public static TasksDTO getTaskByTaskId(int taskId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select * from tasks where id = @taskId";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);

            IDataReader reader = cmd.ExecuteReader();
            TasksDTO task = null;

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"].ToString());
                string taskTitle = reader["title"].ToString();
                string taskStartDate = reader["startDate"].ToString();
                string taskEndDate = reader["endDate"].ToString();
                string taskStatus = reader["status"].ToString();
                bool taskPrivate = Convert.ToBoolean(reader["private"]);
                task = new TasksDTO(id, taskTitle, taskStartDate, taskEndDate, taskStatus, taskPrivate);
            }
            ConnectionDatabase.closeConnection();
            return task;
        }

        public static int updateTask(TasksDTO task, int ownerId, List<int> arrPartnerId)
        {
            ConnectionDatabase.getConnection();

            // update task
            string query = @"update tasks
                             set title=@title,startDate=@startDate,endDate=@endDate,status=@status,private=@private
                             where id=@taskId";

            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@startDate", Convert.ToDateTime(task.startDate));
            cmd.Parameters.AddWithValue("@endDate", Convert.ToDateTime(task.endDate));
            cmd.Parameters.AddWithValue("@status", task.Status);
            cmd.Parameters.AddWithValue("@private", task.Private);
            cmd.Parameters.AddWithValue("@taskId", task.ID);
            cmd.ExecuteNonQuery();

            // remove all usersTasks by taskId
            ConnectionDatabase.getConnection();
            string sql = @"delete from usersTasks
                            where taskId = @taskId";
            cmd = new SqlCommand(sql, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", task.ID);
            cmd.ExecuteNonQuery();

            // add the owner into task
            query = @"insert into usersTasks(userId, taskId, isOwner)
                        values (@userId, @taskId, 1)";
            cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@userId", ownerId);
            cmd.Parameters.AddWithValue("@taskId", task.ID);
            cmd.ExecuteNonQuery();


            // add again by list partnerId
            // add partner into task
            for (int i = 0; i < arrPartnerId.Count; i++)
            {
                query = @"insert into usersTasks(userId, taskId, isOwner)
                        values (@userId, @taskId, 0)";
                cmd = new SqlCommand(query, ConnectionDatabase.conn);
                cmd.Parameters.AddWithValue("@userId", arrPartnerId[i]);
                cmd.Parameters.AddWithValue("@taskId", task.ID);
                cmd.ExecuteNonQuery();
            }

            ConnectionDatabase.closeConnection();
            return 1;
        }

        public static int createComment(int userId, int taskId, string content)
        {
            ConnectionDatabase.getConnection();
            string query = @"insert into comments(userId, taskId, content)
                             values (@userId, @taskId, @content)";

            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.ExecuteNonQuery();
            ConnectionDatabase.closeConnection();
            return 1;
        }

        public static DataTable getCommentByTaskId(int taskId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select name, email, content
                             from  comments c, users u
                             where c.userId = u.id
                             and   taskId = @taskId";

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd = ConnectionDatabase.conn.CreateCommand();
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.CommandText = query;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            ConnectionDatabase.closeConnection();
            return ds.Tables[0];
        }
    }
}