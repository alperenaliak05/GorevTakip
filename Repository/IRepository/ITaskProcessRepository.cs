using Models;

namespace Repositories.IReporsitory
{
    public interface ITaskProcessRepository
    {
        Task<IEnumerable<TaskProcess>> GetTaskProcessesByTaskIdAsync(int taskId);
        Task<bool> AddTaskProcessAsync(TaskProcess taskProcess);
    }
}
