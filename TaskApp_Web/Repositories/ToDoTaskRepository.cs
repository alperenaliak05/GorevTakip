using Microsoft.EntityFrameworkCore;
using TaskAppWeb.Data;
using TaskAppWeb.Models;
using TaskAppWeb.Models.DTO;

namespace TaskAppWeb.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly TaskApp_WebContext _context;

        public ToDoTaskRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<ToDoTasks> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> AddTaskAsync(ToDoTasks task)
        {
            await _context.Tasks.AddAsync(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTaskAsync(ToDoTasks task)
        {
            _context.Tasks.Update(task);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<ToDoTasks>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.AssignedByUser)
                .Where(t => t.AssignedToUserId == userId)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    AssignedByUserFirstName = t.AssignedByUser.FirstName,
                    AssignedByUserLastName = t.AssignedByUser.LastName,
                    DueDate = t.DueDate,
                    Status = t.Status
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId)
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)
                .Where(t => t.AssignedByUserId == userId)
                .Select(t => new TaskTrackingDTO
                {
                    TaskId = (int)t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    AssignedToUserName = t.AssignedToUser.FirstName + " " + t.AssignedToUser.LastName,
                    DueDate = t.DueDate,
                    Status = t.Status
                })
                .ToListAsync();
        }
    }
}
