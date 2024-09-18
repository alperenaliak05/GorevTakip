using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.IServices;
using System.Security.Claims;
using TaskStatus = Models.TaskStatus;


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
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask()
        {
            var users = await _taskService.GetAllUsersAsync();
            var departments = await _taskService.GetAllDepartmentsAsync();

            var userViewModels = users.Select(u => new UserViewModel
            {
                FullName = u.FirstName + " " + u.LastName,
                Id = u.Id
            }).ToList();

            var departmentViewModels = departments.Select(d => new DepartmentViewModel
            {
                Name = d.Name,
                Id = d.Id
            }).ToList();

            var taskViewModel = new TaskViewModel
            {
                Users = userViewModels,
                Departments = departmentViewModels,
                Status = TaskStatus.Bekliyor,
                DueDate = DateTime.Now,
                Priority = TaskPriority.Orta
            };

            return View(taskViewModel);
        }

        [HttpPost]
        [Route("CreateTask")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([FromForm] TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.AssignedToUserId == null && model.AssignedToDepartmentId == null)
                {
                    ModelState.AddModelError("", "Lütfen bir kullanıcı veya departman seçin.");
                    return View(model);
                }

                if (model.AssignedToUserId != null && model.AssignedToDepartmentId != null)
                {
                    ModelState.AddModelError("", "Bir görev ya bir kullanıcıya ya da bir departmana atanabilir, ikisine birden atanamaz.");
                    return View(model);
                }

                if (model.AssignedToUserId != null)
                {
                    var task = new ToDoTasks
                    {
                        Title = model.Title,
                        Description = model.Description,
                        AssignedToUserId = model.AssignedToUserId,
                        AssignedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                        DueDate = model.DueDate,
                        Status = TaskStatus.Bekliyor,
                        Priority = model.Priority
                    };

                    await _taskService.AddTaskAsync(task);
                }

                if (model.AssignedToDepartmentId != null)
                {
                    var usersInDepartment = await _taskService.GetUsersByDepartmentIdAsync(model.AssignedToDepartmentId.Value);

                    foreach (var user in usersInDepartment)
                    {
                        var taskForUser = new ToDoTasks
                        {
                            Title = model.Title,
                            Description = model.Description,
                            AssignedToUserId = user.Id,
                            AssignedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                            DueDate = model.DueDate,
                            Status = TaskStatus.Bekliyor,
                            Priority = model.Priority
                        };

                        await _taskService.AddTaskAsync(taskForUser);
                    }
                }

                return RedirectToAction("TaskTracking");
            }

            var users = await _taskService.GetAllUsersAsync();
            var departments = await _taskService.GetAllDepartmentsAsync();

            model.Users = users.Select(u => new UserViewModel
            {
                FullName = u.FirstName + " " + u.LastName,
                Id = u.Id
            }).ToList();

            model.Departments = departments.Select(d => new DepartmentViewModel
            {
                Name = d.Name,
                Id = d.Id
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
                DueDate = task.DueDate,
                Priority = task.Priority
            }).ToList();

            return View(taskDTOs);
        }

        [HttpPost]
        [Route("CompleteTask/{id}")]
        [ValidateAntiForgeryToken]
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
        [Route("RejectTask/{id}")]
        [ValidateAntiForgeryToken]
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
                Status = task.Status,
                Priority = task.Priority
            };

            return View(model);
        }

        [HttpPost]
        [Route("EditTask/{id}")]
        [ValidateAntiForgeryToken]
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
                task.Priority = model.Priority;

                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction("TaskTracking");
            }

            return View(model);
        }

        [HttpPost]
        [Route("DeleteTask/{id}")]
        [ValidateAntiForgeryToken]
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
                Department = user.Department?.Name ?? "No Department",
                CompletedTasks = tasks.Count(t => t.AssignedToUserId == user.Id && t.Status == TaskStatus.Tamamlandı),
                RejectedTasks = tasks.Count(t => t.AssignedToUserId == user.Id && t.Status == TaskStatus.Reddedildi),
                PendingTasks = tasks.Count(t => t.AssignedToUserId == user.Id && t.Status == TaskStatus.Bekliyor),
            }).ToList();

            return View(taskProgressList);
        }

        [HttpGet]
        [Route("CalendarView")]
        public async Task<IActionResult> CalendarView()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdClaim.Value);

            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            var taskEvents = tasks.Select(task => new TaskCalendarEvent
            {
                Title = task.Title,
                StartDate = task.DueDate.ToString("yyyy-MM-dd"),
                EndDate = task.DueDate.ToString("yyyy-MM-dd")
            }).ToList();

            return View(taskEvents);
        }

        [HttpGet]
        [Route("TaskDetails/{id}")]
        public async Task<IActionResult> TaskDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Geçersiz görev ID'si.");
            }

            var task = await _taskService.GetTaskByIdAsync(id, includeProcesses: true);
            if (task == null)
            {
                return NotFound("Görev bulunamadı.");
            }

            var taskDetailsViewModel = new TaskDetailsViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                Priority = task.Priority,
                AssignedToUser = task.AssignedToUser != null ? $"{task.AssignedToUser.FirstName} {task.AssignedToUser.LastName}" : "Atanmadı",
                AssignedByUser = task.AssignedByUser != null ? $"{task.AssignedByUser.FirstName} {task.AssignedByUser.LastName}" : "Bilinmiyor",
                TaskProcesses = task.TaskProcesses?.ToList() ?? new List<TaskProcess>()
            };

            return View(taskDetailsViewModel);
        }



        [HttpPost]
        [Route("AddTaskProcess")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTaskProcess([FromForm] string processDescription, int id)
        {
            if (id <= 0)
            {
                return BadRequest("Geçersiz görev ID'si.");
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var task = await _taskService.GetTaskByIdAsync(id, includeProcesses: true);
            if (task == null)
            {
                return NotFound("Görev bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(processDescription))
            {
                ModelState.AddModelError("processDescription", "Süreç açıklaması gereklidir.");
                // Hatalı durumda aynı sayfayı aynı model ile göster
                var taskDetailsViewModel = new TaskDetailsViewModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Status = task.Status,
                    Priority = task.Priority,
                    AssignedToUser = task.AssignedToUser != null ? $"{task.AssignedToUser.FirstName} {task.AssignedToUser.LastName}" : "Atanmadı",
                    AssignedByUser = task.AssignedByUser != null ? $"{task.AssignedByUser.FirstName} {task.AssignedByUser.LastName}" : "Bilinmiyor",
                    TaskProcesses = task.TaskProcesses?.ToList() ?? new List<TaskProcess>()
                };
                return View("TaskDetails", taskDetailsViewModel);
            }

            var taskProcess = new TaskProcess
            {
                TaskId = task.Id,
                ProcessDescription = processDescription,
                CreatedAt = DateTime.Now
            };

            await _taskService.AddTaskProcessAsync(taskProcess);

            return RedirectToAction("TaskDetails", new { id = task.Id });
        }
        [HttpGet]
        [Route("TaskTrackingDetails/{id}")]
        public async Task<IActionResult> TaskTrackingDetails(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id, includeProcesses: true);

            if (task == null)
            {
                return NotFound();
            }

            var taskDetailDTO = new TaskTrackingDetailsDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedToUser = task.AssignedToUser != null ? $"{task.AssignedToUser.FirstName} {task.AssignedToUser.LastName}" : "Atanmadı",
                DueDate = task.DueDate,
                Priority = task.Priority,
                Processes = task.TaskProcesses
            };

            return View(taskDetailDTO);
        }



    }
}
