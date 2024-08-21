using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using TaskStatus = TaskApp_Web.Models.TaskStatus;

namespace TaskApp_Web.Repositories.IRepositories
{
    public interface IToDoTaskRepository
    {
        Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
        Task<ToDoTasks> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(ToDoTasks task);
        Task<bool> UpdateTaskAsync(ToDoTasks task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(TaskStatus status);  // Bu satırı ekleyin
        Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId);
    }
}
