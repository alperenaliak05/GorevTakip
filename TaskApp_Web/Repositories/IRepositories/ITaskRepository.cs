using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public interface ITaskRepository : IRepository<ToDoTask>
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync(); 
        Task<ToDoTask> GetTaskByIdAsync(int id);      
        Task AddTaskAsync(ToDoTask task);              
        Task<IEnumerable<ToDoTask>> GetTasksByUserAsync(string userName);
        Task CompleteTaskAsync(int taskId);
        Task IncompleteTaskAsync(int taskId);
        Task SetTaskInProgressAsync(int taskId);
    }

}
