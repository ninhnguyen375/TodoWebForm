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
            }
        }
        private void GridViewTasksDayOfWeek()
        {
            mondayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(-1, 2);
            mondayDataList.DataBind();

            tuesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(-1, 3);
            tuesdayDataList.DataBind();

            wednesdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(-1, 4);
            wednesdayDataList.DataBind();

            thursdayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(-1, 5);
            thursdayDataList.DataBind();

            fridayDataList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(-1, 6);
            fridayDataList.DataBind();

            saturdayDatList.DataSource = TasksBLL.GetAllTasksByUserIdComplyWithDayOfWeek(-1, 7);
            saturdayDatList.DataBind();
        }
    }
}