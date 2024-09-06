namespace Models
{
    public class TaskReport
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public ToDoTasks Task { get; set; }
        public string Report { get; set; }
        public int CreatedByUserId { get; set; }
        public Users CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
