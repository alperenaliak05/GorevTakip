using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using TaskApp_Web.Services.IServices;
using TaskApp_Web.Repositories.IRepositories;

namespace TaskApp_Web.Services
{
    public class TaskReportService : ITaskReportService
    {
        private readonly ITaskReportRepository _taskReportRepository;

        public TaskReportService(ITaskReportRepository taskReportRepository)
        {
            _taskReportRepository = taskReportRepository;
        }

        public async Task<IEnumerable<TaskReportDTO>> GetAllTaskReportsAsync()
        {
            return await _taskReportRepository.GetTaskReportsAsync();
        }

        public async Task<TaskReportDTO> GetTaskReportByIdAsync(int id)
        {
            var report = await _taskReportRepository.GetTaskReportsAsync();
            return report.FirstOrDefault(r => r.TaskId == id);
        }

        public async Task<bool> AddTaskReportAsync(TaskReport taskReport)
        {
            return await _taskReportRepository.AddTaskReportAsync(taskReport);
        }

        public async Task<bool> UpdateTaskReportAsync(TaskReport taskReport)
        {
            return await _taskReportRepository.UpdateTaskReportAsync(taskReport);
        }

        public async Task<bool> DeleteTaskReportAsync(int id)
        {
            return await _taskReportRepository.DeleteTaskReportAsync(id);
        }
    }
}
