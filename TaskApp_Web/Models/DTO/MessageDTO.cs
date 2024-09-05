namespace TaskApp_Web.Models.DTO
{
    public class MessageDTO
    {
        public int SenderId { get; set; }       // Gönderenin ID'si
        public int ReceiverId { get; set; }     // Alıcının ID'si
        public string MessageContent { get; set; }  // Mesaj içeriği
        public DateTime Timestamp { get; set; }   // Mesajın gönderildiği zaman (string olarak formatlanmış)
    }
}
