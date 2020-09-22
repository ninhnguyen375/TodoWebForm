using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoListWebForm.App_Code.DTO
{
    public class TasksDTO
    {
        public TasksDTO()
        {

        }
        public TasksDTO(string title, string startDate, string endDate, string status, bool privateScope)
        {
            this.Title = title;
            this.startDate = startDate;
            this.endDate = endDate;
            this.Status = status;
            this.Private = privateScope;
        }

        public TasksDTO(int id, string title, string startDate, string endDate, string status, bool privateScope)
        {
            this.ID = id;
            this.Title = title;
            this.startDate = startDate;
            this.endDate = endDate;
            this.Status = status;
            this.Private = privateScope;
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public String startDate { get; set; }
        public String endDate { get; set; }
        public String Status { get; set; }
        public bool Private { get; set; }

    }
}