using System;

namespace TaskApp_Web.Models
{
    public class ToDoTasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }

        public int AssignedToUserId { get; set; }
        public Users AssignedToUser { get; set; }

        public int AssignedByUserId { get; set; }
        public Users AssignedByUser { get; set; }
    }
}
