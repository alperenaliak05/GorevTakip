using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Services.IServices;

namespace TaskApp_Web.Services
{
    public class TaskReportService : ITaskReportService
    {
        private readonly TaskApp_WebContext _context;

        public TaskReportService(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskReport>> GetAllTaskReportsAsync()
        {
            return await _context.TaskReports
                .Include(tr => tr.Task)
                .Include(tr => tr.CreatedByUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskReport>> GetTaskReportsByUserTasksAsync(List<int> taskIds)
        {
            return await _context.TaskReports
                .Where(tr => taskIds.Contains(tr.TaskId))
                .Include(tr => tr.Task)
                .Include(tr => tr.CreatedByUser)
                .ToListAsync();
        }

        public async Task<TaskReport> GetTaskReportByIdAsync(int id)
        {
            return await _context.TaskReports
                .Include(tr => tr.Task)
                .Include(tr => tr.CreatedByUser)
                .FirstOrDefaultAsync(tr => tr.Id == id);
        }

        public async Task<bool> AddTaskReportAsync(TaskReport taskReport)
        {
            await _context.TaskReports.AddAsync(taskReport);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTaskReportAsync(TaskReport taskReport)
        {
            _context.TaskReports.Update(taskReport);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskReportAsync(int id)
        {
            var taskReport = await _context.TaskReports.FindAsync(id);

            if (taskReport == null)
            {
                return false;
            }

            _context.TaskReports.Remove(taskReport);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
