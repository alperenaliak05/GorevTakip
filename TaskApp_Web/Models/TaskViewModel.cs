using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace TaskApp_Web.Models
{
    public class TaskViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int AssignedToUserId { get; set; }
        public int AssignedByUserId { get; set; }
        public int Status { get; set; } = (int)TaskStatus.Bekliyor; // Varsayılan Bekliyor
        public string StatusDescription => ((TaskStatus)Status).ToString();
        public string? AssignedByUserFirstName { get; set; }
        public string? AssignedByUserLastName { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today;
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
