using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using TaskApp_Web.Repositories;
using TaskApp_Web.Services.IServices;

namespace TaskApp_Web.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IUserRepository _userRepository;

        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository, IUserRepository userRepository)
        {
            _toDoTaskRepository = toDoTaskRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ToDoTasks>> GetAllTasksAsync()
        {
            return await _toDoTaskRepository.GetAllTasksAsync();
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            return await _toDoTaskRepository.GetTasksByUserIdAsync(userId);
        }

        public async Task<ToDoTasks> GetTaskByIdAsync(int id)
        {
            return await _toDoTaskRepository.GetTaskByIdAsync(id);
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<bool> AddTaskAsync(ToDoTasks task)
        {
            return await _toDoTaskRepository.AddTaskAsync(task);
        }

        public async Task<bool> UpdateTaskAsync(ToDoTasks task)
        {
            return await _toDoTaskRepository.UpdateTaskAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _toDoTaskRepository.DeleteTaskAsync(id);
        }

        // Yeni eklenen metot
        public async Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId)
        {
            return await _toDoTaskRepository.GetTasksAssignedByUserAsync(userId);
        }
    }
}
