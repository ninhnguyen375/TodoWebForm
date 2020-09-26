using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.Constant;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.DTO.Response;

namespace TodoListWebForm
{
    public partial class TaskInWeek : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null)
            {
                Response.Redirect("/login");
            }
            if (!IsPostBack)
            {
                GridViewTasksDayOfWeek(null);
            }
        }
        protected void handleSelectWeek(object sender, EventArgs e)
        {
            string s = selectWeek.Text;

            if (!s.Equals(""))
            {
                string[] split = s.Split('-');
                string year = split[0];
                string Wweek = split[1];
                string week = Wweek.Split('W')[1];

                DateTime date = Helper.FirstDateOfWeekISO8601(Convert.ToInt32(year), Convert.ToInt32(week));
                GridViewTasksDayOfWeek(date.ToString());
            }
            else
            {
                Helper.Toast(this, Page.ClientScript, "warning", "Please select Week");
            }
        }
        private void GridViewTasksDayOfWeek(string datetime)
        {
            int userId = -1;

            if (Session["role"] != null && Session["role"].Equals(Role.Admin))
            {
                userId = -1;
            } else
            {
                userId = Convert.ToInt32(Session["id"]);
            }

            TasksBLL.expiringTask();

            mondayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 2, datetime);
            mondayDataList.DataBind();

            tuesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 3, datetime);
            tuesdayDataList.DataBind();

            wednesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 4, datetime);
            wednesdayDataList.DataBind();

            thursdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 5, datetime);
            thursdayDataList.DataBind();

            fridayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 6, datetime);
            fridayDataList.DataBind();

            saturdayDatList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 7, datetime);
            saturdayDatList.DataBind();
        }
        protected DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        protected void HandleCreateTask(object sender, EventArgs e)
        {
            string titleTask = title.Text;
            String startDateTask = startDate.Text;
            String endDateTask = endDate.Text;
            String statusTask = TaskStatus.InProgress;
            bool IsPrivate = privateScope.Checked;
            string urlFile = null;

            if (fileInput.HasFile)
            {
                try
                {
                    string fileName = Path.GetFileName(fileInput.FileName);
                    fileInput.SaveAs(Server.MapPath("~") + "/Upload/" + fileName);
                    urlFile = "/Upload/" + fileName;
                }
                catch (Exception ex)
                {

                }
            }

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

            TasksDTO task = new TasksDTO(titleTask, startDateTask, endDateTask, statusTask, IsPrivate, urlFile);
            int ownerId = Convert.ToInt32(Session["id"].ToString());
            TasksBLL.CreateTask(task, arr, ownerId);

            TasksBLL.expiringTask();
            GridViewTasksDayOfWeek(null); // get current date time

            Helper.Toast(this, Page.ClientScript, "success", "Create task sussess");
        }
    }
}