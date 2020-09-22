using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.Constant;
using TodoListWebForm.App_Code.DTO.Response;

namespace TodoListWebForm
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void HandleClickRegister(object sender, EventArgs e)
        {
            string name = tbName.Text.Trim();
            string email = tbEmail.Text.Trim();
            string password = tbPassword.Text.Trim();
            string confirmPassword = tbConfirmPassword.Text.Trim();

            if (!password.Equals(confirmPassword))
            {
                lbError.Text = "Incorect confirm password";
                return;
            }

            ResponseRegisterDTO result = AuthBLL.Register(new UsersDTO
            {
                Name = name,
                Email = email,
                Password = password,
                Role = Role.Employee
            });

            if (!result.Success)
            {
                lbError.Text = result.Error;
            }
            else
            {
                Response.Redirect("/login");
            }
        }
    }
}