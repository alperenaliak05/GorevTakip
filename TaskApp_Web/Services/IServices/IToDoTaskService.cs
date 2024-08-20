﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAppWeb.Models;
using TaskAppWeb.Models.DTO;

namespace TaskAppWeb.Services.IServices
{
    public interface IToDoTaskService
    {
        Task<IEnumerable<ToDoTasks>> GetAllTasksAsync();
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);

        Task<ToDoTasks> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(ToDoTasks task);
        Task<bool> UpdateTaskAsync(ToDoTasks task);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<IEnumerable<TaskTrackingDTO>> GetTasksAssignedByUserAsync(int userId);
    }
}
