using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                GridViewTasksDayOfWeek();
                bindDatesOfWeek();
            }
        }
        private void bindDatesOfWeek ()
        {
            DateTime start = StartOfWeek(DateTime.Now, DayOfWeek.Monday);
            DateTime end = StartOfWeek(DateTime.Now, DayOfWeek.Saturday);
            startDateOfWeek.Text = start.ToString().Split(' ')[0];
            endDateOfWeek.Text = end.ToString().Split(' ')[0];
        }
        private void GridViewTasksDayOfWeek()
        {
            int userId = -1;

            if (Session["role"] != null && Session["role"].Equals(Role.Admin))
            {
                userId = -1;
            } else
            {
                userId = Convert.ToInt32(Session["id"]);
            }

            mondayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 2, null);
            mondayDataList.DataBind();

            tuesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 3, null);
            tuesdayDataList.DataBind();

            wednesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 4,null);
            wednesdayDataList.DataBind();

            thursdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 5, null);
            thursdayDataList.DataBind();

            fridayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 6, null);
            fridayDataList.DataBind();

            saturdayDatList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(userId, 7, null);
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

            GridViewTasksDayOfWeek();
        }
    }
}