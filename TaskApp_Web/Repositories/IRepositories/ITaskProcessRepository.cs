using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories.IRepositories
{
    public interface ITaskProcessRepository
    {
        Task<IEnumerable<TaskProcess>> GetTaskProcessesByTaskIdAsync(int taskId);
        Task<bool> AddTaskProcessAsync(TaskProcess taskProcess);
    }
}
