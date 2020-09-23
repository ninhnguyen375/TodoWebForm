using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DAL;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.DTO.Response;

namespace TodoListWebForm.App_Code.BLL
{
    public class TasksBLL
    {
        public static DataTable GetAllTasks()
        {
            return TasksDAL.GetAllTasks();
        }

        public static List<ResponseGetTaskByUserId> GetAllTasksByUserId(int userId)
        {
            List<TasksDTO> arrTasks = TasksDAL.GetAllTasksByUserId(userId);
            List<ResponseGetTaskByUserId> arrFinal = new List<ResponseGetTaskByUserId>();
            // add partner
            for(int i = 0; i < arrTasks.Count; i++)
            {
                DataTable partners = UsersBLL.getListUserOfTask(arrTasks[i].ID);
                ResponseGetTaskByUserId temp = new ResponseGetTaskByUserId();
                temp.ID = arrTasks[i].ID;
                temp.Title = arrTasks[i].Title;
                temp.startDate = arrTasks[i].startDate;
                temp.endDate = arrTasks[i].endDate;
                temp.Status = arrTasks[i].Status;
                temp.Private = arrTasks[i].Private;
                temp.Partners = partners;
                arrFinal.Add(temp);
            }
            return arrFinal;
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

        public static int updateTask(TasksDTO task, List<int> arrPartnerId)
        {
            return TasksDAL.updateTask(task, arrPartnerId);
        }

        public static int createComment(int userId, int taskId, string content)
        {
            return TasksDAL.createComment(userId, taskId, content);
        }
        public static DataTable getCommentByTaskId(int taskId)
        {
            return TasksDAL.getCommentByTaskId(taskId);
        }
    }
}