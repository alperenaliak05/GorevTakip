using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories.IRepositories
{
    public interface ITaskReportRepository
    {
        Task<IEnumerable<TaskReport>> GetTaskReportsByTaskIdAsync(int taskId);
        Task<bool> AddTaskReportAsync(TaskReport report);
        Task<bool> UpdateTaskReportAsync(TaskReport report);
        Task<bool> DeleteTaskReportAsync(int id);
    }
}
