using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;

namespace Repositories
{
    public class TaskProcessRepository : ITaskProcessRepository
    {
        private readonly TaskAppContext _context;

        public TaskProcessRepository(TaskAppContext context)
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
