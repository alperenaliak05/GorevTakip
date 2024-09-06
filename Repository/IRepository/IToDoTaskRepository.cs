using DTOs.TaskApp_WebDTO;
using Models;
using TaskDTO = DTOs.TaskApp_WebDTO.TaskDTO;
using TaskStatus = Models.TaskStatus;

namespace Repositories.IReporsitory;
public interface IToDoTaskRepository
{
    Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
    Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
    Task<ToDoTasks> GetTaskByIdAsync(int id);
    Task<bool> AddTaskAsync(ToDoTasks task);
    Task<bool> UpdateTaskAsync(ToDoTasks task);
    Task<bool> DeleteTaskAsync(int id);
    Task<IEnumerable<ToDoTasks>> GetTasksByStatusAsync(TaskStatus status);
    Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId);
}
