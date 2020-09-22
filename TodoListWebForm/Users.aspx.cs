﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TodoListWebForm.App_Code.BLL;
using TodoListWebForm.App_Code.Constant;
using TodoListWebForm.App_Code.DTO.Request;

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

            if(!IsPostBack)
            {
                gvBind();
            }
        }

        protected void gvBind()
        {
            DataTable data = UsersBLL.getListUsers();
            GridView1.DataSource = data;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            TextBox textName = (TextBox)row.Cells[1].Controls[0];
            string name = textName.Text;
            UsersBLL.updateUser(new UpdateUserDTO { Id = userId, Name = name });
            GridView1.EditIndex = -1;
            gvBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            gvBind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gvBind();
        }
        protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState == DataControlRowState.Edit) ||
               (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)))
            {
                e.Row.Cells[0].Width = Unit.Pixel(35);
                e.Row.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) {event.preventDefault()}");
            }
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if(!IsPostBack)
            {
                return;
            }

            int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            if(Session["id"].Equals(userId))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                    "toastr_message", "toastr.error('Can not delete this user')", true);
                return;
            }

            bool result = UsersBLL.deleteUser(userId);

            if(!result)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                    "toastr_message", "toastr.error('Can not delete this user')", true);
            }
            gvBind();
        }
    }
}