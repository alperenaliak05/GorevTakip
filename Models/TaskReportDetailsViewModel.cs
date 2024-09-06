namespace DTO
{
    public class TaskReportDetailsViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public bool CanAddReport { get; set; }
        public string ReportContent { get; set; }
    }
}
