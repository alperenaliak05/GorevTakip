using Microsoft.AspNetCore.Mvc;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using System.Security.Claims;
using TaskApp_Web.Services.IServices;
using TaskStatus = TaskApp_Web.Models.TaskStatus;
using Models;

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
                    Status = (int)TaskStatus.Bekliyor, // Status Bekliyor olarak ayarlandı
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

        // Tamamlandı butonu için action metodu
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tasks/CompleteTask/{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task != null)
            {
                task.Status = (int)TaskStatus.Tamamlandı;
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
                task.Status = (int)TaskStatus.Reddedildi; 
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("MyTasks");
            }
            return NotFound();
        }
    }
}
