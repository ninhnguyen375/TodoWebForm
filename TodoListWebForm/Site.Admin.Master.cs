using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TodoListWebForm
{
    public partial class Site_Admin : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void HandleClickLogout(object sender, EventArgs e)
        {
            if (Session["name"] == null)
            {
                return;
            }

            Session.Abandon();
            Response.Redirect("/");
        }
    }
}