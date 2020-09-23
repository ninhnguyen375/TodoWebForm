using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class UsersDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] == null)
            {
                Response.Redirect("/login");
            }
            if (Session["role"] != null && !Session["role"].Equals(Role.Admin))
            {
                Response.Redirect("/login");
            }

            int userId = Convert.ToInt32(Request.QueryString["id"]);
            UsersDTO user = UsersBLL.getUserById(userId);

            if(user == null)
            {
                Response.Redirect("/users");
            }

            lbEmail.Text = user.Email;
            lbName.Text = user.Name;
            lbRole.Text = user.Role;

            List<ResponseTaskDTO> tasksOfUser = UsersBLL.getTasksOfUser(userId);
            DataList1.DataSource = tasksOfUser;
            DataList1.DataBind();
        }
    }
}