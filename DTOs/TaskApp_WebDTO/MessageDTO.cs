namespace DTOs.TaskApp_WebDTO
{
    public class MessageDTO
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string SenderName { get; set; } // Gönderen ismi
        public string ReceiverName { get; set; } // Alıcı ismi
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
