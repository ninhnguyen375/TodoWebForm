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
            if (Session["name"] == null)
            {
                Response.Redirect("/login");
            }

            getValueTask();

            if (task == null)
            {
                Response.Redirect("/tasks");
            }

            if (!IsPostBack)
            {
                initValue();
                bindComment();
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
            bindPartner();


            List<int> arrPartnerId = UsersBLL.getListPartnerIdFollowTaskId(task.ID);

            // check partners
            foreach (GridViewRow row in usersGridView.Rows)
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

        protected void handleUpdateTaskDetail(object sender, EventArgs e)
        {
            // get info task
            string titleTask = title.Text;
            String startDateTask = startDate.Text;
            String endDateTask = endDate.Text;
            String statusTask = status.Value;
            bool IsPrivate = privateScope.Checked;

            TasksDTO newTask = new TasksDTO(task.ID, titleTask, startDateTask, endDateTask, statusTask, IsPrivate);

            // get list partner
            List<int> arr = new List<int>();
            foreach (GridViewRow row in usersGridView.Rows)
            {

                CheckBox checkBox = (CheckBox)row.FindControl("checkbox");
                if (checkBox.Checked)
                {
                    arr.Add(Int32.Parse(usersGridView.DataKeys[row.RowIndex].Value.ToString()));
                }
            }
            TasksBLL.updateTask(newTask, arr);

            getValueTask();
            initValue();
        }

        private void bindPartner()
        {
            if (Session["id"] == null)
            {
                Response.Redirect("/login");
            }
            // load partners 
            usersGridView.DataSource = UsersBLL.getListUsersExceptRoleAdmin();
            usersGridView.DataBind();
        }

        private void bindComment()
        {
            commentsDataList.DataSource = TasksBLL.getCommentByTaskId(task.ID);
            commentsDataList.DataBind();
        }

        private void getValueTask()
        {
            int taskId = Convert.ToInt32(Request.QueryString["id"]);
            task = TasksBLL.getTaskByTaskId(taskId);
        }

        protected void handleSubmitComment(object sender, EventArgs e)
        {
            string content = chatBox.Text;
            TasksBLL.createComment(Convert.ToInt32(Session["id"].ToString()), task.ID, content);
            bindComment();

            // reset 
            chatBox.Text = "";
        }

        protected void HandleDeleteTask(object sender, EventArgs e)
        {
            int result = TasksBLL.DeleteTaskById(Convert.ToString(task.ID));
            Response.Redirect("/tasks");
        }

        protected void handleRemindTask(object sender, EventArgs e)
        {
            TasksBLL.createComment(Convert.ToInt32(Session["id"].ToString()), task.ID, @"
            DM CHÚNG M CÓ CÁI TASK MÀ LÀM CHƯA XONG
            ");
            bindComment();

            // reset 
            chatBox.Text = "";
        }
    }
}