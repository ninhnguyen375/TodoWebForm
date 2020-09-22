using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.DTO.Response;

namespace TodoListWebForm.App_Code.BLL
{
    public class AuthBLL
    {
        public static UsersDTO Login(string email, string password)
        {
            return DAL.AuthDAL.Login(email, password);
        }
        public static ResponseRegisterDTO Register(UsersDTO user)
        {
            return DAL.AuthDAL.Register(user);
        }
    }
}