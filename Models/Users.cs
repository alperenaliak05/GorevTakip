﻿namespace Models
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
        public string? PhoneNumber { get; set; }
        public string? WorkingHours { get; set; }
        public string? ProfilePicture { get; set; }
        public int CompletedTasksCount { get; set; }
        public UsersStatus Status { get; set; }
        public ICollection<ToDoTasks> Tasks { get; set; }
        public ICollection<ToDoTasks> AssignedTasks { get; set; }
        public ICollection<UserToDoList> ToDoLists { get; set; }
        public ICollection<TaskReport> TaskReports { get; set; }
        public ICollection<ToDoTasks> CreatedTasks { get; set; }
        public ICollection<TaskReport> CreatedReports { get; set; }
        public ICollection<UserBadge> UserBadges { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }

    }
}
