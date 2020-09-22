using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.Constant;

namespace TodoListWebForm
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["name"] == null)
            {
                Response.Redirect("/login");
            }
            if (Session["role"] != null && !Session["role"].Equals(Role.Admin))
            {
                Response.Redirect("/login");
            }

            DataTable data = UsersBLL.getListUsers();
            GridView1.DataSource = data;
            GridView1.DataBind();
        }
    }
}