using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskApp_Web.Data;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly TaskApp_WebContext _context;

        public ToDoTaskRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoTasks>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.ToDoTasks
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .Where(t => t.AssignedToUserId == userId)
                .ToListAsync();
        }

        public async Task<ToDoTasks> GetTaskByIdAsync(int id)
        {
            return await _context.ToDoTasks
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> AddTaskAsync(ToDoTasks task)
        {
            await _context.ToDoTasks.AddAsync(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTaskAsync(ToDoTasks task)
        {
            _context.ToDoTasks.Update(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _context.ToDoTasks.Remove(task);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public Task<IEnumerable<ToDoTasks>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }
    }
}
