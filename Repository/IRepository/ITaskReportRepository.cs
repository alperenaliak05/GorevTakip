using DTOs.TaskApp_WebDTO;
using Models;

namespace Repositories.IReporsitory
{
    public interface ITaskReportRepository
    {
        Task<IEnumerable<TaskReportDTO>> GetTaskReportsAsync();
        Task<IEnumerable<TaskReportDTO>> GetTaskReportsByUserIdAsync(int userId); // Kullanıcıya göre raporları getiren metod
        Task<bool> AddTaskReportAsync(TaskReport report);
        Task<bool> UpdateTaskReportAsync(TaskReport report);
        Task<bool> DeleteTaskReportAsync(int id);
    }
}
