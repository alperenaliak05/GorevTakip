using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
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

        public async Task<IEnumerable<TaskReportDTO>> GetTaskReportsAsync()
        {
            return await _context.TaskReports
                .Include(tr => tr.Task)
                .Include(tr => tr.CreatedByUser)
                .Select(tr => new TaskReportDTO
                {
                    TaskId = tr.TaskId,
                    TaskTitle = tr.Task.Title,
                    TaskDescription = tr.Task.Description,
                    AssignedByUserFirstName = tr.Task.AssignedByUser.FirstName,
                    AssignedByUserLastName = tr.Task.AssignedByUser.LastName,
                    DueDate = tr.Task.DueDate,
                    TaskStatus = tr.Task.Status,
                    ReportContent = tr.Report,  // Rapor içeriği doğru alan adı
                    CreatedAt = tr.CreatedAt,
                    CreatedByUserId = tr.CreatedByUserId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskReportDTO>> GetTaskReportsByUserIdAsync(int userId)
        {
            return await _context.TaskReports
                .Include(tr => tr.Task)
                .Include(tr => tr.CreatedByUser)
                .Where(tr => tr.CreatedByUserId == userId)
                .Select(tr => new TaskReportDTO
                {
                    TaskId = tr.TaskId,
                    TaskTitle = tr.Task.Title,
                    TaskDescription = tr.Task.Description,
                    AssignedByUserFirstName = tr.Task.AssignedByUser.FirstName,
                    AssignedByUserLastName = tr.Task.AssignedByUser.LastName,
                    DueDate = tr.Task.DueDate,
                    TaskStatus = tr.Task.Status,
                    ReportContent = tr.Report,
                    CreatedAt = tr.CreatedAt,
                    CreatedByUserId = tr.CreatedByUserId
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
