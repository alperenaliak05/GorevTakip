namespace TaskApp_Web.Models
{
    public class TaskViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today; // Varsayılan olarak bugünün tarihi
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public string? Process { get; set; } // Process alanını null olarak bırakabiliriz
        public int? AssignedToUserId { get; set; } // Kullanıcı atanabilir veya null olabilir
        public int? AssignedToDepartmentId { get; set; } // Departman atanabilir veya null olabilir
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>(); // Varsayılan boş liste
        public List<DepartmentViewModel> Departments { get; set; } = new List<DepartmentViewModel>(); // Varsayılan boş liste
    }
}
