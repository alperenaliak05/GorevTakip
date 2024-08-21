using Microsoft.AspNetCore.Mvc;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using System.Security.Claims;
using TaskApp_Web.Services.IServices;
using Models;
using TaskStatus = TaskApp_Web.Models.TaskStatus;

namespace TaskApp_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : Controller
    {
        private readonly IToDoTaskService _taskService;

        public ToDoTaskController(IToDoTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            var users = await _taskService.GetAllUsersAsync();

            var userViewModels = users.Select(u => new UserViewModel
            {
                FullName = u.FirstName + " " + u.LastName,
                Id = u.Id
            }).ToList();

            var taskViewModel = new TaskViewModel
            {
                Users = userViewModels,
                Status = (int)TaskStatus.Bekliyor,
            };

            return View(taskViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([FromForm] TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = new ToDoTasks
                {
                    Title = model.Title,
                    Description = model.Description,
                    AssignedToUserId = model.AssignedToUserId,
                    AssignedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    DueDate = model.DueDate,
                    Status = (int)TaskStatus.Bekliyor,
                };

                await _taskService.AddTaskAsync(task);
                return RedirectToAction("MyTasks");
            }

            var users = await _taskService.GetAllUsersAsync();
            model.Users = users.Select(u => new UserViewModel
            {
                FullName = u.FirstName + " " + u.LastName,
                Email = u.Email
            }).ToList();

            return View(model);
        }

        [HttpGet]
        [Route("MyTasks")]
        public async Task<IActionResult> MyTasks()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdClaim.Value);

            var tasks = await _taskService.GetTasksByUserIdAsync(userId);

            var taskDTOs = tasks.Select(task => new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedToUserId = task.AssignedToUserId,
                AssignedByUserId = task.AssignedByUserId,
                AssignedByUserFirstName = task.AssignedByUserFirstName,
                AssignedByUserLastName = task.AssignedByUserLastName,
                DueDate = task.DueDate
            }).ToList();

            return View(taskDTOs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tasks/CompleteTask/{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task != null)
            {
                task.Status = TaskStatus.Tamamlandı;
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("MyTasks");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tasks/RejectTask/{id}")]
        public async Task<IActionResult> RejectTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task != null)
            {
                task.Status = TaskStatus.Reddedildi;
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("MyTasks");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("TaskTracking")]
        public async Task<IActionResult> TaskTracking()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdClaim.Value);
            var tasks = await _taskService.GetTasksAssignedByUserAsync(userId);
            return View(tasks);
        }

        [HttpGet]
        [Route("EditTask/{id}")]
        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            var model = new TaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditTask/{id}")]
        public async Task<IActionResult> EditTask(int id, [FromForm] TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null) return NotFound();

                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate;
                task.Status = model.Status;

                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("TaskTracking");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction("TaskTracking");
        }
        [HttpGet]
        [Route("TaskProgress")]
        public async Task<IActionResult> TaskProgress()
        {
            var users = await _taskService.GetAllUsersAsync();
            var tasks = await _taskService.GetAllTasksAsync();

            var taskProgressList = users.Select(user => new TaskProgressViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                Department = user.Department?.Name ?? "No Department", // Adjusting for null values
                CompletedTasks = tasks.Count(t => t.AssignedToUserId == user.Id && t.Status == TaskStatus.Tamamlandı),
                RejectedTasks = tasks.Count(t => t.AssignedToUserId == user.Id && t.Status == TaskStatus.Reddedildi),
                PendingTasks = tasks.Count(t => t.AssignedToUserId == user.Id && t.Status == TaskStatus.Bekliyor),
            }).ToList();

            return View(taskProgressList);
        }

    }
}
