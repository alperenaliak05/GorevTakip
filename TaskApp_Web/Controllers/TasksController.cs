using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories;

namespace TaskApp_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IActionResult TaskList()
        {
            var tasks = _taskRepository.GetAllTasksAsync();
            return View(tasks);
        }

        public IActionResult TaskDetails(int id)
        {
            var task = _taskRepository.GetTaskByIdAsync(id);
            return View(task);
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.AddTaskAsync(task);
                return RedirectToAction("TaskList");
            }
            return View(task);
        }
    }

}
