using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TaskApp_Web.Models;
using TaskApp_Web.Services;
using TaskApp_Web.Services.IServices;

namespace TaskApp_Web.Controllers
{
    [Authorize]
    public class TaskReportController : Controller
    {
        private readonly ITaskReportService _taskReportService;
        private readonly IToDoTaskService _taskService;
        private readonly IUsersService _userService;

        public TaskReportController(ITaskReportService taskReportService, IToDoTaskService taskService, IUsersService userService)
        {
            _taskReportService = taskReportService;
            _taskService = taskService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdClaim.Value);
            var user = await _userService.GetUserByIdAsync(userId);
            var userDepartment = user.Department.Name;

            if (userDepartment == "Analist")
            {
                var allReports = await _taskReportService.GetAllTaskReportsAsync();
                return View(allReports);
            }
            else
            {
                var userTasks = await _taskService.GetTasksByUserIdAsync(userId);
                var taskIds = userTasks.Select(t => t.Id.Value).ToList();
                var userReports = await _taskReportService.GetTaskReportsByUserTasksAsync(taskIds);
                return View(userReports);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddReport()
        {
            ViewBag.Tasks = new SelectList(await _taskService.GetAllTasksAsync(), "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(TaskReport taskReport)
        {
            if (ModelState.IsValid)
            {
                taskReport.CreatedAt = DateTime.Now;
                taskReport.CreatedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                await _taskReportService.AddTaskReportAsync(taskReport);
                return RedirectToAction("Index");
            }
            ViewBag.Tasks = new SelectList(await _taskService.GetAllTasksAsync(), "Id", "Title");
            return View(taskReport);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var report = await _taskReportService.GetTaskReportByIdAsync(id);
            if (report == null) return NotFound();

            ViewBag.Tasks = new SelectList(await _taskService.GetAllTasksAsync(), "Id", "Title", report.TaskId);
            return View(report);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskReport taskReport)
        {
            if (ModelState.IsValid)
            {
                taskReport.UpdatedAt = DateTime.Now;

                await _taskReportService.UpdateTaskReportAsync(taskReport);
                return RedirectToAction("Index");
            }
            ViewBag.Tasks = new SelectList(await _taskService.GetAllTasksAsync(), "Id", "Title", taskReport.TaskId);
            return View(taskReport);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskReportService.DeleteTaskReportAsync(id);
            return RedirectToAction("Index");
        }
    }
}
