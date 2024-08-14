using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public interface IToDoTaskRepository
    {
        Task<IEnumerable<ToDoTasks>> GetTasksByUserIdAsync(int userId);
        Task<ToDoTasks> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(ToDoTasks task);
        Task<bool> UpdateTaskAsync(ToDoTasks task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
    }
}
