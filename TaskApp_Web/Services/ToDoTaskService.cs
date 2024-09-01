using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using TaskApp_Web.Repositories;
using TaskApp_Web.Repositories.IRepositories;
using TaskApp_Web.Services.IServices;

namespace TaskApp_Web.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITaskProcessRepository _taskProcessRepository; // Yeni eklenen repository

        public ToDoTaskService(
            IToDoTaskRepository toDoTaskRepository,
            IUserRepository userRepository,
            IDepartmentRepository departmentRepository,
            ITaskProcessRepository taskProcessRepository) // Yeni repository parametresi
        {
            _toDoTaskRepository = toDoTaskRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _taskProcessRepository = taskProcessRepository; // Atama
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

        public async Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(Models.TaskStatus status)
        {
            return await _toDoTaskRepository.GetTasksByStatusAsync(status);
        }

        public async Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId)
        {
            return await _toDoTaskRepository.GetTasksAssignedByUserAsync(userId);
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllDepartmentsAsync();
        }

        public async Task<IEnumerable<Users>> GetUsersByDepartmentIdAsync(int departmentId)
        {
            return await _userRepository.GetUsersByDepartmentIdAsync(departmentId);
        }

        // Yeni eklenen metot: Belirli bir görevin tüm süreç kayıtlarını getirir
        public async Task<IEnumerable<TaskProcess>> GetTaskProcessesByTaskIdAsync(int taskId)
        {
            return await _taskProcessRepository.GetTaskProcessesByTaskIdAsync(taskId);
        }

        // Yeni eklenen metot: Yeni bir süreç kaydı ekler
        public async Task<bool> AddTaskProcessAsync(TaskProcess taskProcess)
        {
            return await _taskProcessRepository.AddTaskProcessAsync(taskProcess);
        }
    }
}
