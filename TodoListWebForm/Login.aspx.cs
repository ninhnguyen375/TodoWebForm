using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.Constant;
using TodoListWebForm.App_Code.DTO;

namespace TodoListWebForm
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbError.Visible = false;
        }

        protected void HandleClickLogin(object sender, EventArgs e)
        {
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Text.Trim();

            UsersDTO result = AuthBLL.Login(email, password);

            if (result != null)
            {
                Session["name"] = result.Name;
                Session["email"] = result.Email;
                Session["role"] = result.Role;

                if(result.Role.Equals(Role.Admin))
                {
                    Response.Redirect("/dashboard");

                } else
                {
                    Response.Redirect("/");
                }
            }
            else
            {
                lbError.Visible = true;
            }
        }
    }
}