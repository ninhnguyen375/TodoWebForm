using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DTO
{
    public class UsersDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}