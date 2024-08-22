using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;
using TaskApp_Web.Data;
using TaskApp_Web.Models.DTO;
using Models;

namespace TaskApp_Web.Repositories
{
    public class TaskReportRepository : ITaskReportRepository
    {
        private readonly TaskApp_WebContext _context;

        public TaskReportRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskReportDTO>> GetTaskReportsAsync()
        {
            return await _context.TaskReports
                
                .Include(tr => tr.Task)
                .ThenInclude(tr => tr.AssignedByUser) // AssignedByUser'ı Include ediyoruz
                .Select(tr => new TaskReportDTO
                {
                    TaskId = tr.TaskId,
                    TaskTitle = tr.Task.Title,
                    TaskDescription = tr.Task.Description,
                    AssignedByUserFirstName = tr.Task.AssignedByUser.FirstName,
                    AssignedByUserLastName = tr.Task.AssignedByUser.LastName,
                    DueDate = tr.Task.DueDate,
                    TaskStatus = tr.Task.Status
                })
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
