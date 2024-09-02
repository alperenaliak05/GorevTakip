using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using TaskStatus = TaskApp_Web.Models.TaskStatus;

namespace TaskApp_Web.Services.IServices
{
    public interface IToDoTaskService
    {
        Task<ToDoTasks> GetTaskByIdAsync(int id, bool includeProcesses = false);
        Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
        Task<ToDoTasks> GetTaskByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<bool> AddTaskAsync(ToDoTasks task);
        Task<bool> UpdateTaskAsync(ToDoTasks task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(TaskStatus status);
        Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId);
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync();
        Task<IEnumerable<Users>> GetUsersByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<TaskProcess>> GetTaskProcessesByTaskIdAsync(int taskId);
        Task<bool> AddTaskProcessAsync(TaskProcess taskProcess);
        Task<bool> CompleteTaskAsync(int taskId); // Yeni eklenen metot
        Task UpdateUserCompletedTasksCountAsync(int userId); // Kullanıcının tamamladığı görev sayısını güncellemek için
    }
}
