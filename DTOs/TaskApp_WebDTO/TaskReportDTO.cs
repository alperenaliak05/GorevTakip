namespace DTOs.TaskApp_WebDTO
{
    public class TaskReportDTO
    {
        public int? TaskId { get; set; } 
        public string TaskTitle { get; set; }  
        public string TaskDescription { get; set; }  
        public string AssignedByUserFirstName { get; set; }  
        public string AssignedByUserLastName { get; set; }  
        public DateTime DueDate { get; set; }  
        public TaskStatus TaskStatus { get; set; }  
        public string ReportContent { get; set; }  
        public DateTime CreatedAt { get; set; }  
        public int CreatedByUserId { get; set; }  
    }
}
