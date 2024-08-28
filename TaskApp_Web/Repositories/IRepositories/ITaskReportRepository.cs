using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;

namespace TaskApp_Web.Repositories.IRepositories
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
