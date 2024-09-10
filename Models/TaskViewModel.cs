namespace Models
{
    public class TaskViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today; 
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string? Process { get; set; } 
        public int? AssignedToUserId { get; set; } 
        public int? AssignedToDepartmentId { get; set; } 
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>(); 
        public List<DepartmentViewModel> Departments { get; set; } = new List<DepartmentViewModel>(); 
    }
}
