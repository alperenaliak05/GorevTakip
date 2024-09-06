using Models;
using TaskStatus = Models.TaskStatus;

namespace DTOs.TaskApp_WebDTO;
public class TaskDTO
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? AssignedToUserId { get; set; }
    public int? AssignedByUserId { get; set; }
    public TaskPriority Priority { get; set; }
    public string? AssignedByUserFirstName { get; set; } // Yeni eklenen alan
    public string? AssignedByUserLastName { get; set; }  // Yeni eklenen alan
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public string? Process { get; set; }
}

