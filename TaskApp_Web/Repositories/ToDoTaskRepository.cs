using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using TaskApp_Web.Repositories.IRepositories;

namespace TaskApp_Web.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly TaskApp_WebContext _context;

        public ToDoTaskRepository(TaskApp_WebContext context)
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
                .Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    AssignedToUserId = t.AssignedToUserId,
                    AssignedByUserId = t.AssignedByUserId,
                    AssignedByUserFirstName = t.AssignedByUser.FirstName,
                    AssignedByUserLastName = t.AssignedByUser.LastName,
                    DueDate = t.DueDate,
                    Status = t.Status
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

        public async Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(Models.TaskStatus status)
        {
            return await _context.Tasks
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
                    Status = t.Status
                })
                .ToListAsync();
        }

    }
}
