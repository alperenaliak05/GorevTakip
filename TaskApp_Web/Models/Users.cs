using System.Collections.Generic;
using TaskApp_Web.Models;

namespace TaskApp_Web.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }
        public string? Gender { get; set; }
        public ICollection<ToDoTasks> Tasks { get; set; }
        public ICollection<ToDoTasks> AssignedTasks { get; set; }
        public ICollection<UserToDoList> ToDoLists { get; set; }
        public ICollection<TaskReport> TaskReports { get; set; }
    }
}
