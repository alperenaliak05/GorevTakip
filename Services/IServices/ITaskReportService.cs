using DTOs.TaskApp_WebDTO;
using Models;

namespace Services.IServices
{
    public interface ITaskReportService
    {
        Task<IEnumerable<TaskReportDTO>> GetAllTaskReportsAsync();
        Task<TaskReportDTO> GetTaskReportByIdAsync(int id);
        Task<bool> AddTaskReportAsync(TaskReport taskReport);
        Task<bool> UpdateTaskReportAsync(TaskReport taskReport);
        Task<bool> DeleteTaskReportAsync(int id);
        Task<IEnumerable<TaskReportDTO>> GetTaskReportsByUserIdAsync(int userId);

    }
}
