﻿namespace TaskApp_Web.Models.DTO
{
    public class TaskReportDTO
    {
        public int? TaskId { get; set; }
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string AssignedByUserFirstName { get; set; }
        public string AssignedByUserLastName { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus TaskStatus { get; set; }
    }
}
