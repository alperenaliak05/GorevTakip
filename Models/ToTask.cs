namespace Models
{
    public class ToTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; } 
        public int AssignedByUserId { get; set; }
        public User AssignedByUser { get; set; } 
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}
