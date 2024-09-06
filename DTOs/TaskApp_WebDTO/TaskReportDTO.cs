namespace DTOs.TaskApp_WebDTO
{
    public class TaskReportDTO
    {
        public int? TaskId { get; set; }  // Görev ID'si
        public string TaskTitle { get; set; }  // Görev başlığı
        public string TaskDescription { get; set; }  // Görev açıklaması
        public string AssignedByUserFirstName { get; set; }  // Görevi atan kullanıcının adı
        public string AssignedByUserLastName { get; set; }  // Görevi atan kullanıcının soyadı
        public DateTime DueDate { get; set; }  // Görevin bitiş tarihi
        public TaskStatus TaskStatus { get; set; }  // Görevin durumu
        public string ReportContent { get; set; }  // Rapor içeriği (Doğru alan adı)
        public DateTime CreatedAt { get; set; }  // Raporun oluşturulma tarihi
        public int CreatedByUserId { get; set; }  // Raporu oluşturan kullanıcının ID'si
    }
}
