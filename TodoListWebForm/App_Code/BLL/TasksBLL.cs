using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DAL;
using TodoListWebForm.App_Code.DTO;

namespace TodoListWebForm.App_Code.BLL
{
    public class TasksBLL
    {
        public static DataTable GetAllTasks()
        {
            return TasksDAL.GetAllTasks();
        }

        public static DataTable GetAllTasksByUserId(int userId)
        {
            return TasksDAL.GetAllTasksByUserId(userId);
        }

        public static int CreateTask(TasksDTO task, List<int> arrUser, int ownerId)
        {
            return TasksDAL.CreateTask(task, arrUser, ownerId);
        }

        public static int DeleteTaskById(string id)
        {
            return TasksDAL.DeleteTaskById(id);
        }

        public static TasksDTO getTaskByTaskId(int taskId)
        {
            return TasksDAL.getTaskByTaskId(taskId);
        }
    }
}