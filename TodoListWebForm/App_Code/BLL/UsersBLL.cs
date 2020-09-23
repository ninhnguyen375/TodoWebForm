using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DAL;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.DTO.Request;
using TodoListWebForm.App_Code.DTO.Response;

namespace TodoListWebForm.App_Code.BLL
{
    public class UsersBLL
    {
        public static DataTable getListUsers()
        {
            return UsersDAL.getListUsers();
        }

        public static DataTable getListUsersExceptCurrentUser(int currentUserId)
        {
            return UsersDAL.getListUsersExceptCurrentUser(currentUserId);
        }
        public static void updateUser(UpdateUserDTO user)
        {
            UsersDAL.updateUser(user);
        }
        public static bool deleteUser(int id)
        {
            return UsersDAL.deleteUser(id);
        }
        public static UsersDTO getUserById (int id)
        {
            return UsersDAL.getUserById(id);
        }
        public static List<ResponseTaskDTO> getTasksOfUser(int userId)
        {
            DataTable tableTasks = UsersDAL.getTasksOfUser(userId);
            List<ResponseTaskDTO> tasks = new List<ResponseTaskDTO>();

            foreach (DataRow row in tableTasks.Rows)
            {
                int taskId = Convert.ToInt32(row.ItemArray.GetValue(0));
                DataTable usersOfTask = getListUserOfTask(taskId);
                tasks.Add(new ResponseTaskDTO
                {
                    ID = Convert.ToInt32(row.ItemArray.GetValue(0)),
                    Title = row.ItemArray.GetValue(1).ToString(),
                    startDate = row.ItemArray.GetValue(2).ToString(),
                    endDate = row.ItemArray.GetValue(3).ToString(),
                    Status = row.ItemArray.GetValue(4).ToString(),
                    Private= Convert.ToBoolean(row.ItemArray.GetValue(5)),
                    Users = usersOfTask
                });

            }
            return tasks;
        }

        public static List<int> getListPartnerIdFollowTaskId(int taskId)
        {
            return UsersDAL.getListPartnerIdFollowTaskId(taskId);
        }
        public static DataTable getListUserOfTask(int taskId)
        {
            return UsersDAL.getListUserOfTask(taskId);
        }
    }
}