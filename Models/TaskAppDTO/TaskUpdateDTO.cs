namespace Models.TaskAppDTO
{
    internal class TaskUpdateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
    }
}
