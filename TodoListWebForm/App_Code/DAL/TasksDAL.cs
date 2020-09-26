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
                                            order by case when status = 'expired' then 1
			                                  when status = 'inprogress' then 2
			                                  when status = 'done' then 3
			                                  else 4
		                                    end asc, startDate desc";
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
                    string startDate = reader.GetValue(2).ToString().Split(Convert.ToChar(" "))[0];
                    string endDate = reader.GetValue(3).ToString().Split(Convert.ToChar(" "))[0]; 
                    string status = reader.GetString(4);
                    bool Private = Convert.ToBoolean(reader.GetValue(5));
                    string urlFile = reader["urlFile"].ToString();
                    arr.Add(new TasksDTO(id,
                                         title,
                                         startDate,
                                         endDate,
                                         status,
                                         Private,
                                         urlFile));
                }
                reader.NextResult();
            }
            return arr;
        }

        public static int CreateTask(TasksDTO task, List<int> arrUser, int ownerId)
        {
            ConnectionDatabase.getConnection();
            string query;
            if(task.urlFile == null)
            {
                query = @"insert into tasks(title, startDate, endDate, status, private)
                            values(@title, @startDate, @endDate, @status, @private); 
                            select MAX(id) FROM tasks";
            } else
            {
                query = @"insert into tasks(title, startDate, endDate, status, private, urlFile)
                            values(@title, @startDate, @endDate, @status, @private, @urlFile); 
                            select MAX(id) FROM tasks";
            }
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@startDate", Convert.ToDateTime(task.startDate));
            cmd.Parameters.AddWithValue("@endDate", Convert.ToDateTime(task.endDate));
            cmd.Parameters.AddWithValue("@status", task.Status);
            cmd.Parameters.AddWithValue("@private", task.Private);
            if(task.urlFile != null)
            {
                cmd.Parameters.AddWithValue("@urlFile", task.urlFile);
            }

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
                string urlFile = reader["urlFile"].ToString();
                task = new TasksDTO(id, taskTitle, taskStartDate, taskEndDate, taskStatus, taskPrivate, urlFile);
            }
            ConnectionDatabase.closeConnection();
            return task;
        }

        public static int updateTask(TasksDTO task, List<int> arrPartnerId)
        {
            int ownerId = getOwnerByTaskId(task.ID);
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

            // remove owner checked in arrPartnerId cause owner is inserted above
            arrPartnerId.Remove(ownerId);

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

        public static int getOwnerByTaskId(int taskId)
        {
            ConnectionDatabase.getConnection();

            string query = @"select * from usersTasks
                             where taskId in (select id 
                                              from tasks
                                              where id = @taskId)
                             and isOwner=1";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            IDataReader reader = cmd.ExecuteReader();
            int userId = -1;
            while (reader.Read())
            {
                userId = Convert.ToInt32(reader["userId"]);
            }
            ConnectionDatabase.closeConnection();
            return userId;
        }

        public static int createComment(int userId, int taskId, string content, bool isRemind)
        {
            ConnectionDatabase.getConnection();
            string query = @"insert into comments(userId, taskId, content, isRemind)
                             values (@userId, @taskId, @content, @isRemind)";

            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@isRemind", isRemind);
            cmd.ExecuteNonQuery();
            ConnectionDatabase.closeConnection();
            return 1;
        }

        public static DataTable getCommentByTaskId(int taskId)
        {
            ConnectionDatabase.getConnection();
            string query = @"select *
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
        public static void updateStatusOfTask(int taskId, string status)
        {
            ConnectionDatabase.getConnection();
            string query = @"update tasks
                             set status=@status
                             where id=@taskId";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);
            cmd.Parameters.AddWithValue("@status", status);

            cmd.ExecuteNonQuery();

            ConnectionDatabase.closeConnection();
        }

        public static List<TasksDTO> GetAllTasksByUserIdComplyWithDayOfWeek(int userId, int day_of_week, string datetime)
        {
            ConnectionDatabase.getConnection();
            string query;

            if(userId != -1)
            {
                query = @"select * from (
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
                                 where DATEPART(WEEKDAY,startDate) = @day_of_week
                                 and startDate >= (SELECT  DATEADD(DAY, 2- DATEPART(WEEKDAY, @datetime), CAST(@datetime AS DATE)) [WeekStart])
	                             and startDate <= (SELECT  DATEADD(DAY, 7 - DATEPART(WEEKDAY, @datetime), CAST(@datetime AS DATE)) [WeekEnd])
                                 order by case when status = 'expired' then 1
			                          when status = 'inprogress' then 2
			                          when status = 'done' then 3
			                          else 4
		                         end asc, startDate desc";

            }
            else
            {
                query = @"select * from (select * 
                                         from tasks) as temp
                                 where DATEPART(WEEKDAY,startDate) = @day_of_week
                                 and startDate >= (SELECT  DATEADD(DAY, 2- DATEPART(WEEKDAY, @datetime), CAST(@datetime AS DATE)) [WeekStart])
	                             and startDate <= (SELECT  DATEADD(DAY, 7 - DATEPART(WEEKDAY, @datetime), CAST(@datetime AS DATE)) [WeekEnd])
                                 order by case when status = 'expired' then 1
			                          when status = 'inprogress' then 2
			                          when status = 'done' then 3
			                          else 4
		                         end asc, startDate desc";
            }
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@day_of_week", day_of_week);

            if(datetime == null)
            {
                cmd.Parameters.AddWithValue("@datetime", DateTime.Now.ToString("MM/dd/yyyy"));
            }else
            {
                cmd.Parameters.AddWithValue("@datetime", Convert.ToDateTime(datetime).ToString("MM/dd/yyyy"));
            }

            SqlDataReader reader = cmd.ExecuteReader();
            List<TasksDTO> arr = new List<TasksDTO>();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string startDate = reader.GetValue(2).ToString().Split(Convert.ToChar(" "))[0];
                    string endDate = reader.GetValue(3).ToString().Split(Convert.ToChar(" "))[0];
                    string status = reader.GetString(4);
                    bool Private = Convert.ToBoolean(reader.GetValue(5));
                    string urlFile = reader["urlFile"].ToString();
                    arr.Add(new TasksDTO(id,
                                         title,
                                         startDate,
                                         endDate,
                                         status,
                                         Private,
                                         urlFile));
                }
                reader.NextResult();
            }
            return arr;
        }
        public static bool isTaskHasRemind(int taskId)
        {
            ConnectionDatabase.getConnection();

            string query = @"select 1
                            from comments
                            where taskId = @taskId
	                            and isRemind = 1";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@taskId", taskId);

            int rowEffected = Convert.ToInt32(cmd.ExecuteScalar());

            ConnectionDatabase.closeConnection();

            if (rowEffected > 0)
            {
                return true;
            }

            return false;
        }
        public static void expiringTask ()
        {
            ConnectionDatabase.getConnection();

            string query = @"update tasks
                            set status = 'expired'
                            where id in (select id
			                            from tasks
			                            where endDate < Convert(date, getdate())
				                            and status = 'inprogress')";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.ExecuteNonQuery();

            ConnectionDatabase.closeConnection();
        }
    }
}