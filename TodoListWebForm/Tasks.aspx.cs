using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                GridViewTasksDayOfWeek(null); // get current date time
            }
        }

        private void GridViewTasksDayOfWeek(string datetime)
        {
            //mondayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(Convert.ToInt32(Session["id"].ToString()), 2, datetime);
            //mondayDataList.DataBind();

            //tuesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(Convert.ToInt32(Session["id"].ToString()), 3, datetime);
            //tuesdayDataList.DataBind();

            //wednesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(Convert.ToInt32(Session["id"].ToString()), 4, datetime);
            //wednesdayDataList.DataBind();

            //thursdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(Convert.ToInt32(Session["id"].ToString()), 5, datetime);
            //thursdayDataList.DataBind();

            //fridayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(Convert.ToInt32(Session["id"].ToString()), 6, datetime);
            //fridayDataList.DataBind();

            //saturdayDatList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(Convert.ToInt32(Session["id"].ToString()), 7, datetime);
            //saturdayDatList.DataBind();
        }

        protected void HandleCreateTask(object sender, EventArgs e)
        {
            string titleTask = title.Text;
            String startDateTask = startDate.Text;
            String endDateTask = endDate.Text;
            String statusTask = TaskStatus.InProgress;
            bool IsPrivate = privateScope.Checked;
            string urlFile = null;

            if(fileInput.HasFile)
            {
                try
                {
                    string fileName = Path.GetFileName(fileInput.FileName);
                    fileInput.SaveAs(Server.MapPath("~") + "/Upload/" + fileName);
                    urlFile = "/Upload/" + fileName;
                }   catch (Exception ex)
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
            GridViewTaskBind();
            GridViewUserBind();
            GridViewTasksDayOfWeek(null); // get current date time

            Helper.Toast(this, Page.ClientScript, "success", "Create task sussess");
        }

        protected void HandleDeleteTask(object sender, GridViewDeleteEventArgs e)
        {
            string taskId = tasksGridView.DataKeys[e.RowIndex].Value.ToString();
            int result = TasksBLL.DeleteTaskById(taskId);
            GridViewTaskBind();
        }

        private void GridViewTaskBind()
        {
            TasksBLL.expiringTask();

            tasksGridView.DataSource = TasksBLL.GetAllTasksByUserId(Convert.ToInt32(Session["id"].ToString()));
            tasksGridView.DataBind();
        }

        private void GridViewUserBind()
        {
            usersGridView.DataSource = UsersBLL.getListUsersExceptCurrentUser(Convert.ToInt32(Session["id"].ToString()));
            usersGridView.DataBind();
        }

        protected void handleSelectWeek (object sender, EventArgs e)
        {
            //string s = selectWeek.Text;

            //if(!s.Equals(""))
            //{
            //    string[] split = s.Split('-');
            //    string year = split[0];
            //    string Wweek = split[1];
            //    string week = Wweek.Split('W')[1];

            //    DateTime date = FirstDateOfWeekISO8601(Convert.ToInt32(year), Convert.ToInt32(week));
            //    GridViewTasksDayOfWeek(date.ToString());
            //}else
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
            //        "toastr_message", "toastr.error('Please choose day')", true);
            //}
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
    }
}