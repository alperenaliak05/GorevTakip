using Microsoft.AspNetCore.Mvc;
using TaskApp_Web.Models;
using TaskApp_Web.Models.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskApp_Web.Services.IServices;

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

        public async Task<IActionResult> TaskList()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }

        public async Task<IActionResult> TaskDetails(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            var users = await _taskService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] ToDoTasks task)
        {
            if (ModelState.IsValid)
            {
                task.AssignedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _taskService.AddTaskAsync(task);
                return RedirectToAction("TaskList");
            }

            var users = await _taskService.GetAllUsersAsync();
            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName + " " + u.LastName
            }).ToList();

            return View(task);
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
                Status = (int)task.Status,
                AssignedToUserId = task.AssignedToUserId,
                AssignedByUserId = task.AssignedByUserId,
                AssignedByUserFirstName = task.AssignedByUserFirstName,
                AssignedByUserLastName = task.AssignedByUserLastName,
                DueDate = task.DueDate
            }).ToList();

            return View(taskDTOs);
        }
    }
}
