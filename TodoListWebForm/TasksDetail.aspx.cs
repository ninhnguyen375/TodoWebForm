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
            startDate.Text = DateTime.Parse(task.startDate).ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            endDate.Text = DateTime.Parse(task.endDate).ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            status.Value = task.Status;
            privateScope.Checked = task.Private;

            // load partners 
            usersGridView.DataSource = UsersBLL.getListUsersExceptCurrentUser(Convert.ToInt32(Session["id"].ToString()));
            usersGridView.DataBind();


            List<int> arrPartnerId = UsersBLL.getListPartnerIdFollowTaskId(task.ID);

            // check partners
            foreach(GridViewRow row in usersGridView.Rows)
            {
                int partnerCurrentId = Convert.ToInt32(usersGridView.DataKeys[row.RowIndex].Value.ToString());
                int index = arrPartnerId.FindIndex(u => u == partnerCurrentId);
                if (index != -1)
                {
                    CheckBox ckb = row.FindControl("checkbox") as CheckBox;
                    ckb.Checked = true;
                }
                
            }
        }

    }
}