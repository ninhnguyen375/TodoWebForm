using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DTO.Response
{
    public class ResponseTaskDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String Status { get; set; }
        public bool Private { get; set; }
        public DataTable Users { get; set; }
    }
}