using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskApp_WebContext _context;

        public TaskRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<ToDoTask> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTaskAsync(ToDoTask task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTask>> GetTasksByUserAsync(string userName)
        {
            return await _context.Tasks
                .Where(t => t.AssignedToUser.Email == userName)
                .ToListAsync();
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = "Completed";
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncompleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = "Incomplete";
                await _context.SaveChangesAsync();
            }
        }

        public async Task SetTaskInProgressAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = "In Progress";
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<ToDoTask>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ToDoTask> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToDoTask>> FindAsync(Expression<Func<ToDoTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(ToDoTask entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ToDoTask entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
