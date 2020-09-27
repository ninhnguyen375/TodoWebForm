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

        List<int> arrPartnerId = new List<int>();

        public bool isOwner;

        public bool isPartner;
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
                getValueTask();
                initValue();
                renderActionButtons();
                loadDisEnInput();
                bindComment();
            }
        }

        private void initValue()
        {
            title.Text = task.Title;
            startDate.Text = DateTime.Parse(task.startDate).ToLocalTime().ToString("yyyy-MM-dd");
            endDate.Text = DateTime.Parse(task.endDate).ToLocalTime().ToString("yyyy-MM-dd");
            status.Value = task.Status;
            privateScope.Checked = task.Private;
            attachedFile.Text = task.urlFile;
            attachedFile.NavigateUrl = "~" + task.urlFile;

            // load partners 
            bindPartner();

            arrPartnerId = UsersBLL.getListPartnerIdFollowTaskId(task.ID);

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

            TasksDTO newTask = new TasksDTO(task.ID, titleTask, startDateTask, endDateTask, statusTask, IsPrivate, task.urlFile);

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

            Helper.Toast(this, Page.ClientScript, "success", "Update task success");
            TasksBLL.expiringTask();
            renderActionButtons();
            getValueTask();
            initValue();
            renderActionButtons();
            loadDisEnInput();
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
            TasksBLL.expiringTask();

            int taskId = Convert.ToInt32(Request.QueryString["id"]);
            task = TasksBLL.getTaskByTaskId(taskId);
        }

        protected void handleSubmitComment(object sender, EventArgs e)
        {
            string content = chatBox.Text;
            if(string.IsNullOrEmpty(content))
            {
                Helper.Toast(this, Page.ClientScript, "info", "Please input comment");
                return;
            }
            TasksBLL.createComment(Convert.ToInt32(Session["id"].ToString()), task.ID, content, false);
            bindComment();

            // reset 
            chatBox.Text = "";
        }

        protected void HandleDeleteTask(object sender, EventArgs e)
        {
            int result = TasksBLL.DeleteTaskById(Convert.ToString(task.ID));
            Response.Redirect("/taskinweek");
        }

        protected void handleRemindTask(object sender, EventArgs e)
        {
            string content = chatBox.Text;
            TasksBLL.createComment(Convert.ToInt32(Session["id"].ToString()), task.ID, content, true);
            bindComment();

            // reset 
            chatBox.Text = "";
        }

        protected void renderActionButtons()
        {
            int userId = Convert.ToInt32(Session["id"]);

            // Check current user exists in the list of partner or not
            int partnerIndex = arrPartnerId.FindIndex(u => u == userId);

            // Get owner id 
            int ownerId = TasksBLL.getOwnerByTaskId(task.ID);

            if (partnerIndex != -1)
            {
                isPartner = true;
            } else
            {
                isPartner = false;
            }

            if (Convert.ToInt32(Session["id"].ToString()) == ownerId)
            {
                isOwner = true;
                isPartner = false;
            } else
            {
                isOwner = false;
            }
        }

        protected void loadDisEnInput()
        {
            if (!Session["role"].Equals("admin") && !isOwner && !isPartner)
            {
                title.Enabled = false;
                startDate.Enabled = false;
                endDate.Enabled = false;
                status.Disabled = true;
                privateScope.Enabled = false;

                foreach(GridViewRow row in usersGridView.Rows)
                {
                    var checkbox = (CheckBox) row.FindControl("checkbox");
                    
                    if (!checkbox.Checked)
                    {
                        checkbox.Enabled = false;
                    } else
                    {
                        checkbox.Style.Add("pointer-events", "none");
                    }
                }
            } else 
            {
                // Get owner id 
                int ownerId = TasksBLL.getOwnerByTaskId(task.ID);

               
                    foreach (GridViewRow row in usersGridView.Rows)
                    {
                        var checkbox = (CheckBox) row.FindControl("checkbox");

                        if (checkbox.Checked && Convert.ToInt32(row.Cells[1].Text) == ownerId)
                        {
                            checkbox.Style.Add("pointer-events", "none");
                        }
                    }
            }
        }
    }
}