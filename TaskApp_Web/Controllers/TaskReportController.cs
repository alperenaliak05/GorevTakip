using DTO;
using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Services.IServices;
using System.Security.Claims;
using TaskStatus = Models.TaskStatus;


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
            // Tamamlanan görevleri çekiyoruz
            var completedTasks = await _taskService.GetTasksByStatusAsync(TaskStatus.Tamamlandı);

            // Görevler raporlarıyla birlikte View'a gönderiliyor
            var taskReports = completedTasks.Select(task => new TaskReportDTO
            {
                TaskId = task.Id,
                TaskTitle = task.Title,
                TaskDescription = task.Description,
                AssignedByUserFirstName = task.AssignedByUserFirstName,
                AssignedByUserLastName = task.AssignedByUserLastName,

                DueDate = task.DueDate,
                TaskStatus = (System.Threading.Tasks.TaskStatus)task.Status
            }).ToList();

            return View(taskReports);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            // Geri kalan işlemler...
            var model = new TaskReportDetailsViewModel
            {
                TaskId = task.Id,
                TaskTitle = task.Title,
                TaskDescription = task.Description,
                DueDate = task.DueDate,
                TaskStatus = (System.Threading.Tasks.TaskStatus)task.Status,
                CanAddReport = (task.AssignedByUserId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReport(TaskReportDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = await _taskService.GetTaskByIdAsync(model.TaskId);

                if (task == null || task.AssignedByUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                {
                    return Unauthorized();
                }

                var report = new TaskReport
                {
                    TaskId = model.TaskId,
                    Report = model.ReportContent,
                    CreatedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    CreatedAt = DateTime.Now
                };

                await _taskReportService.AddTaskReportAsync(report);
                return RedirectToAction("Index");
            }

            return View("Details", model);
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskReportService.DeleteTaskReportAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> MyReports()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var reports = await _taskReportService.GetTaskReportsByUserIdAsync(userId);

            var reportDTOs = reports.Select(report => new TaskReportDTO
            {
                TaskId = report.TaskId,
                TaskTitle = report.TaskTitle,
                TaskDescription = report.TaskDescription,
                DueDate = report.DueDate,
                TaskStatus = report.TaskStatus,
                ReportContent = report.ReportContent,
                CreatedAt = report.CreatedAt
            }).ToList();

            return View(reportDTOs);
        }

    }

}
