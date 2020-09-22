using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DTO.Request
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}