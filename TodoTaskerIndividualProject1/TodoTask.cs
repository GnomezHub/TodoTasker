using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDoTasker
{
    public enum TaskStatus { Todo, Done }

    public class TodoTask
    {
        public TodoTask(string title, string project, DateTime dueDate, TaskStatus status)
        {
            Title = title ?? "";
            Project = project ?? "";
            DueDate = dueDate;
            Status = status;
        }

        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public string Project { get; set; }
    }
}