using TaskApp_Web.Models.DTO;
using TaskApp_Web.Models;
using TaskStatus = TaskApp_Web.Models.TaskStatus;

public interface IToDoTaskRepository
{
    Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
    Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
    Task<ToDoTasks> GetTaskByIdAsync(int id);
    Task<bool> AddTaskAsync(ToDoTasks task);
    Task<bool> UpdateTaskAsync(ToDoTasks task);
    Task<bool> DeleteTaskAsync(int id);
    Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(TaskStatus status);  // Bu metod doğru
    Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId);
}
