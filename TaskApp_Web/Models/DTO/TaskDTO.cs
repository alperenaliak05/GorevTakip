﻿namespace TaskApp_Web.Models.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }
        public int AssignedByUserId { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
    }
}
