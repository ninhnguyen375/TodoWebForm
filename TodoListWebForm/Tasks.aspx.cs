using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.Constant;
using TodoListWebForm.App_Code.DTO;

namespace TodoListWebForm
{
    public partial class Tasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Convert.ToString(Session["id"]);

            if (String.IsNullOrEmpty(user))
            {
                Response.Redirect("/login");
            }

            if (!IsPostBack)
            {
                GridViewTaskBind();
                GridViewUserBind();
            }
        }

        protected void HandleCreateTask(object sender, EventArgs e)
        {
            string titleTask = title.Text;
            String startDateTask = startDate.Text;
            String endDateTask = endDate.Text;
            String statusTask = TaskStatus.InProgress;
            bool IsPrivate = privateScope.Checked;

            // find list selected user
            List<int> arr = new List<int>();
            foreach (GridViewRow row in usersGridView.Rows)
            {

                CheckBox checkBox = (CheckBox)row.FindControl("checkbox");
                if (checkBox.Checked)
                {
                    arr.Add(Int32.Parse(usersGridView.DataKeys[row.RowIndex].Value.ToString()));
                }
            }

            TasksDTO task = new TasksDTO(titleTask, startDateTask, endDateTask, statusTask, IsPrivate);
            int ownerId = Convert.ToInt32(Session["id"].ToString());
            TasksBLL.CreateTask(task, arr, ownerId);
            GridViewTaskBind();
        }

        protected void HandleDeleteTask(object sender, GridViewDeleteEventArgs e)
        {
            string taskId = tasksGridView.DataKeys[e.RowIndex].Value.ToString();
            int result = TasksBLL.DeleteTaskById(taskId);
            GridViewTaskBind();
        }

        private void GridViewTaskBind()
        {
            tasksGridView.DataSource = TasksBLL.GetAllTasksByUserId(Convert.ToInt32(Session["id"].ToString()));
            tasksGridView.DataBind();
        }

        private void GridViewUserBind()
        {
            usersGridView.DataSource = UsersBLL.getListUsersExceptCurrentUser(Convert.ToInt32(Session["id"].ToString()));
            usersGridView.DataBind();
        }
    }
}