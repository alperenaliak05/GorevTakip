using DTOs.TaskApp_WebDTO;
using Models;
using Repositories;
using Repositories.IReporsitory;
using Services.IServices;
using TaskStatus = Models.TaskStatus;

namespace Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly ToDoTaskRepository _toDoTaskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITaskProcessRepository _taskProcessRepository;

        public ToDoTaskService(
            IToDoTaskRepository toDoTaskRepository,
            IUserRepository userRepository,
            IDepartmentRepository departmentRepository,
            ITaskProcessRepository taskProcessRepository)
        {
            _toDoTaskRepository = (ToDoTaskRepository?)toDoTaskRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _taskProcessRepository = taskProcessRepository;
        }

        public async Task<IEnumerable<ToDoTasks>> GetAllTasksAsync()
        {
            return await _toDoTaskRepository.GetAllTasksAsync();
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            return await _toDoTaskRepository.GetTasksByUserIdAsync(userId);
        }

        public async Task<ToDoTasks> GetTaskByIdAsync(int id, bool includeProcesses = false)
        {
            if (includeProcesses)
            {
                var task = await _toDoTaskRepository.GetTaskByIdAsync(id);
                if (task != null)
                {
                    task.TaskProcesses = (await _taskProcessRepository.GetTaskProcessesByTaskIdAsync(id)).ToList();
                }
                return task;
            }
            else
            {
                return await _toDoTaskRepository.GetTaskByIdAsync(id);
            }
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

        public async Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(TaskStatus status)
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

        public async Task<IEnumerable<TaskProcess>> GetTaskProcessesByTaskIdAsync(int taskId)
        {
            return await _taskProcessRepository.GetTaskProcessesByTaskIdAsync(taskId);
        }

        public async Task<bool> AddTaskProcessAsync(TaskProcess taskProcess)
        {
            return await _taskProcessRepository.AddTaskProcessAsync(taskProcess);
        }

        public async Task<bool> CompleteTaskAsync(int taskId)
        {
            var task = await _toDoTaskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return false;

            task.Status = TaskStatus.Tamamlandı; // Görev durumunu güncelle

            var result = await _toDoTaskRepository.UpdateTaskAsync(task);

            if (result)
            {
                // Kullanıcının tamamladığı görev sayısını güncelle
                await UpdateUserCompletedTasksCountAsync(task.AssignedToUserId);
            }

            return result;
        }

        public async Task UpdateUserCompletedTasksCountAsync(int? userId)
        {
            if (!userId.HasValue)
            {
                return;
            }

            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user != null)
            {
                user.CompletedTasksCount++;
                await _userRepository.UpdateUserAsync(user);
            }
        }

        public Task UpdateUserCompletedTasksCountAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
