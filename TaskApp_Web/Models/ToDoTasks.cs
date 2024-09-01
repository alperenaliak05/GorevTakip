using Models;
using System;
using TaskApp_Web.Models;

namespace TaskApp_Web.Models
{
    public class ToDoTasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? AssignedToDepartmentId { get; set; }
        public Users? AssignedToUser { get; set; }
        public int AssignedByUserId { get; set; }
        public Users AssignedByUser { get; set; }
        public ICollection<TaskReport> TaskReports { get; set; }
        public TaskPriority? Priority { get; set; }
        public string? AssignedByUserFirstName => AssignedByUser?.FirstName;
        public string? AssignedByUserLastName => AssignedByUser?.LastName;
        public ICollection<TaskProcess> TaskProcesses { get; set; } = new List<TaskProcess>();
    }

}
