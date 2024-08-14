using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories;
using TaskApp_Web.Models.DTO;
using System.Linq;
using System.Security.Claims;

namespace TaskApp_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : Controller
    {
        private readonly IToDoTaskRepository _taskRepository;

        public ToDoTaskController(IToDoTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IActionResult> TaskList()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return View(tasks);
        }

        public async Task<IActionResult> TaskDetails(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            return View(task);
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(ToDoTasks task)
        {
            if (ModelState.IsValid)
            {
                await _taskRepository.AddTaskAsync(task);
                return RedirectToAction("TaskList");
            }
            return View(task);
        }

        [HttpGet]
        [Route("MyTasks")]
        public async Task<IActionResult> MyTasks()
        {
            // Token veya Cookie'den kullanıcının kimliğini al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                // Kullanıcı kimliği bulunamazsa, hata döndür veya oturum açma sayfasına yönlendir
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdClaim.Value); // Kimlik değeri integer ise parse edilir

            // Kullanıcıya ait tüm görevleri veritabanından al
            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);

            // Görevleri DTO formatında bir listeye dönüştür
            var taskDTOs = tasks.Select(task => new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = (int)task.Status,  // Açık tür dönüşümü yapılıyor
                AssignedToUserId = task.AssignedToUserId,
                AssignedByUserId = task.AssignedByUserId,
                DueDate = task.DueDate
            }).ToList();


            // MyTasks Razor View'a yönlendir
            return View(taskDTOs);
        }
    }
}
