using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;
using TaskApp_Web.Data;

namespace TaskApp_Web.Repositories
{
    public class TaskReportRepository : ITaskReportRepository
    {
        private readonly TaskApp_WebContext _context;

        public TaskReportRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskReport>> GetTaskReportsByTaskIdAsync(int taskId)
        {
            return await _context.TaskReports
                .Where(r => r.TaskId == taskId)
                .Include(r => r.CreatedByUser)
                .ToListAsync();
        }

        public async Task<bool> AddTaskReportAsync(TaskReport report)
        {
            _context.TaskReports.Add(report);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTaskReportAsync(TaskReport report)
        {
            _context.TaskReports.Update(report);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskReportAsync(int id)
        {
            var report = await _context.TaskReports.FindAsync(id);
            if (report != null)
            {
                _context.TaskReports.Remove(report);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
