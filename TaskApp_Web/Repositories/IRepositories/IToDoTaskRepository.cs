using TaskAppWeb.Models;
using TaskAppWeb.Models.DTO;

namespace TaskAppWeb.Repositories
{
    public interface IToDoTaskRepository
    {
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
        Task<ToDoTasks> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(ToDoTasks task);
        Task<bool> UpdateTaskAsync(ToDoTasks task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
        Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId);
    }
}
