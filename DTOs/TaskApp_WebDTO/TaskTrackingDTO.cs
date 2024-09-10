using Models;
using TaskStatus = Models.TaskStatus;

namespace DTOs.TaskApp_WebDTO
{
    public class TaskTrackingDTO
    {
        public int? TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedToUserName { get; set; } 
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; } 
    }
}
