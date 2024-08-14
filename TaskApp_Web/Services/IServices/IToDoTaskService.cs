using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;

namespace TaskApp_Web.Services.IServices
{
    public interface IToDoTaskService
    {
        Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
        Task<IEnumerable<ToDoTasks>> GetTasksByUserIdAsync(int userId);
        Task<ToDoTasks> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(ToDoTasks task);
        Task<bool> UpdateTaskAsync(ToDoTasks task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
