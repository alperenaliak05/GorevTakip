namespace Models.TaskAppDTO
{
    public class TaskCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
