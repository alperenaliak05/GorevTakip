using Models;

namespace TaskAppWeb.Models.DTO
{
    public class TaskTrackingDTO
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedToUserName { get; set; } // Görevi atanan kullanıcının adı
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; } // Görevin durumu
    }
}
