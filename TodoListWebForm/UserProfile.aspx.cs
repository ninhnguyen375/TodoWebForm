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
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["id"] == null)
            {
                Response.Redirect("/login");
            }

            UsersDTO user = UsersBLL.getUserById(Convert.ToInt32(Session["id"]));

            lbUserEmail.Text = user.Email;
            lbUserName.Text = user.Name;
        }
    }
}