using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.DTO;

namespace TodoListWebForm
{
    public partial class UsersDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            UsersDTO user = UsersBLL.getUserById(userId);

            if(user == null)
            {
                Response.Redirect("/users");
            }

            lbEmail.Text = user.Email;
            lbName.Text = user.Name;
            lbRole.Text = user.Role;

            DataTable tasksOfUser = UsersBLL.getTasksOfUser(userId);
            GridTasks.DataSource = tasksOfUser;
            GridTasks.DataBind();
        }
    }
}