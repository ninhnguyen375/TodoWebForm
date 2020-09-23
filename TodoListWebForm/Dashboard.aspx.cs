using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.Constant;

namespace TodoListWebForm
{
    public partial class Dashboard : System.Web.UI.Page
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
        }
    }
}