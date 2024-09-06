using Data;
using DTOs.TaskApp_WebDTO;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;
using TaskStatus = Models.TaskStatus;
using Task = System.Threading.Tasks.Task;



namespace Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly TaskAppContext _context;

        public ToDoTaskRepository(TaskAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoTasks>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.AssignedToUserId == userId)
                .Include(t => t.AssignedByUser)
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    AssignedToUserId = t.AssignedToUserId ?? 0,
                    AssignedByUserId = t.AssignedByUserId,
                    AssignedByUserFirstName = t.AssignedByUser.FirstName,
                    AssignedByUserLastName = t.AssignedByUser.LastName,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    Priority = t.Priority
                })
                .ToListAsync();
        }

        public async Task<ToDoTasks> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.AssignedByUser)
                .Include(t => t.AssignedToUser)
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
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(TaskStatus status)
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)  // Kullanıcıya atanan görev
                .Include(t => t.AssignedByUser)  // Görevi atayan kullanıcı
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.AssignedByUserId == userId)
                .Select(t => new TaskTrackingDTO
                {
                    TaskId = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    AssignedToUserName = t.AssignedToUser.FirstName + " " + t.AssignedToUser.LastName,
                    DueDate = t.DueDate,
                    Status = (System.Threading.Tasks.TaskStatus)t.Status  // Status'u açıkça Models.TaskStatus olarak belirtiyoruz
                })
                .ToListAsync();
        }

    }
}
