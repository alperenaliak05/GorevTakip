namespace TaskApp_Web.Models
{
    public class TaskDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority? Priority { get; set; }
        public string AssignedToUser { get; set; }
        public string AssignedByUser { get; set; }
        public string Process { get; set; }
        public List<TaskProcess> TaskProcesses { get; set; }
    }

}
