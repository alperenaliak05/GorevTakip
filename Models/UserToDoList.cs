namespace Models
{
    public class UserToDoList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
