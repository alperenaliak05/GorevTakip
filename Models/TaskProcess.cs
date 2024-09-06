namespace Models
{
    public class TaskProcess
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string ProcessDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ToDoTasks Task { get; set; }
    }
}
