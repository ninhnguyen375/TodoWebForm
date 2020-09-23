using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DTO.Response
{
    public class ResponseGetTaskByUserId
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string Status { get; set; }
        public bool Private { get; set; }
        public DataTable Partners { get; set; }
    }
}