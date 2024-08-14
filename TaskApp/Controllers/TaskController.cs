﻿using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskAppContext _context;

    public TasksController(TaskAppContext context)
    {
        _context = context;
    }

    // Görev alan kişinin altındaki görevleri ve görevi atan kişinin ismini listeleme
    [HttpGet("assigned/{userId}")]
    public async Task<IActionResult> GetTasksForUser(int userId)
    {
        var tasks = await _context.Tasks
            .Where(task => task.AssignedToUserId == userId)
            .Include(task => task.AssignedByUser) // Görevi atan kişiyi de dahil et
            .ToListAsync();

        return Ok(tasks);
    }

    // Görev atayan kişinin görev atadığı kullanıcıları listeleme
    [HttpGet("assigner/{assignerId}")]
    public async Task<IActionResult> GetUsersAssignedTasks(int assignerId)
    {
        var users = await _context.Users
            .Where(user => user.Tasks.Any(task => task.AssignedByUserId == assignerId))
            .ToListAsync();

        return Ok(users);
    }
}