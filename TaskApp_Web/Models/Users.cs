using System.Collections.Generic;

namespace TaskAppWeb.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }
        public ICollection<ToDoTasks> Tasks { get; set; }
        public ICollection<ToDoTasks> AssignedTasks { get; set; }
        public ICollection<UserToDoList> ToDoLists { get; set; }

    }
}
