using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;

namespace TaskApp_Web.Services.IServices
{
    public interface ITaskReportService
    {
        Task<IEnumerable<TaskReportDTO>> GetAllTaskReportsAsync();
        Task<TaskReportDTO> GetTaskReportByIdAsync(int id);
        Task<bool> AddTaskReportAsync(TaskReport taskReport);
        Task<bool> UpdateTaskReportAsync(TaskReport taskReport);
        Task<bool> DeleteTaskReportAsync(int id);
    }
}
