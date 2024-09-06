using Models;
using TaskStatus = Models.TaskStatus;

namespace DTOs.TaskApp_WebDTO
{
    public class TaskTrackingDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public string AssignedToUser { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public IEnumerable<TaskProcess> Processes { get; set; }
    }
}
