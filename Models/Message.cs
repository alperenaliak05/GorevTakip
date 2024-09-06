namespace Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        public Users Sender { get; set; }
        public int ReceiverId { get; set; }
        public Users Receiver { get; set; }
    }
}
