namespace Models
{
    public class TaskProgressViewModel
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public int CompletedTasks { get; set; }
        public int RejectedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverdueTasks { get; set; }
    }
}
