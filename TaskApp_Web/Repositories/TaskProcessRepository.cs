using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;

namespace TaskApp_Web.Repositories
{
    public class TaskProcessRepository : ITaskProcessRepository
    {
        private readonly TaskApp_WebContext _context;

        public TaskProcessRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskProcess>> GetTaskProcessesByTaskIdAsync(int taskId)
        {
            return await _context.TaskProcesses
                .Where(tp => tp.TaskId == taskId)
                .ToListAsync();
        }

        public async Task<bool> AddTaskProcessAsync(TaskProcess taskProcess)
        {
            _context.TaskProcesses.Add(taskProcess);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
