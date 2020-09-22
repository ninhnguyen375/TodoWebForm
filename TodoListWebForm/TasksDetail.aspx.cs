using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.DTO;

namespace TodoListWebForm
{
    public partial class TasksDetail : System.Web.UI.Page
    {
        TasksDTO task;
        protected void Page_Load(object sender, EventArgs e)
        {
            int taskId = Convert.ToInt32(Request.QueryString["id"]);
            task = TasksBLL.getTaskByTaskId(taskId);

            if (task == null)
            {
                Response.Redirect("/tasks");
            }

            if (!IsPostBack)
            {
                initValue();
            }
        }

        private void initValue()
        {
            title.Text = task.Title;
        }

    }
}