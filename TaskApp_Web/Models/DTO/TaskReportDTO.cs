namespace TaskApp_Web.Models.DTO
{
    public class TaskReportDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Report { get; set; }  // Rapor içeriği
        public int CreatedByUserId { get; set; }
        public string CreatedByUserFullName { get; set; }  // Raporu oluşturan kullanıcının tam adı
        public DateTime CreatedAt { get; set; }  // Raporun oluşturulma tarihi
        public DateTime? UpdatedAt { get; set; }  // Raporun güncellenme tarihi, nullable olabilir
    }
}
