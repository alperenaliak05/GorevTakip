using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;

namespace TaskApp_Web.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly TaskApp_WebContext _context;

        public ToDoTaskRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        // Bu metot IToDoTaskRepository arayüzüyle uyumludur ve doğru şekilde implemente edilmiştir.



        public async Task<ToDoTasks> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.AssignedToUser)
                .Include(t => t.AssignedByUser)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
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
               .Where(t => t.AssignedToUserId == userId)
               .Select(t => new TaskDTO
               {
                   Id = t.Id,
                   Title = t.Title,
                   Description = t.Description,
                   AssignedToUserId = t.AssignedToUserId,
                   AssignedByUserId = t.AssignedByUserId,
                   DueDate = t.DueDate,
                   Status = (int)t.Status // Enum'dan int'e açık dönüşüm yapılıyor
               })
               .ToListAsync();
        }
    }
}
