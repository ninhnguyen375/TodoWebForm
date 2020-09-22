using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DAL;

namespace TodoListWebForm.App_Code.BLL
{
    public class UsersBLL
    {
        public static DataTable getListUsers()
        {
            return UsersDAL.getListUsers();
        }
    }
}