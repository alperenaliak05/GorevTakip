using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.IReporsitory;
using System.Security.Claims;

namespace TaskApp_Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserToDoListRepository _userToDoListRepository;

        public ProfileController(IUserToDoListRepository userToDoListRepository)
        {
            _userToDoListRepository = userToDoListRepository;
        }

        public async Task<IActionResult> UserToDoList()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userToDoLists = await _userToDoListRepository.GetToDoListsByUserIdAsync(userId);
            return View("UserToDoList", userToDoLists);
        }

        [HttpPost]
        public async Task<IActionResult> AddToDoList(UserToDoList model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                model.CreatedAt = DateTime.Now;
                await _userToDoListRepository.AddToDoListAsync(model);
                return RedirectToAction("UserToDoList");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userToDoLists = await _userToDoListRepository.GetToDoListsByUserIdAsync(userId);
            return View("UserToDoList", userToDoLists);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateToDoList(UserToDoList model)
        {
            if (ModelState.IsValid)
            {
                await _userToDoListRepository.UpdateToDoListAsync(model);
                return RedirectToAction("UserToDoList");
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userToDoLists = await _userToDoListRepository.GetToDoListsByUserIdAsync(userId);
            return View("UserToDoList", userToDoLists);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteToDoList(int id)
        {
            await _userToDoListRepository.DeleteToDoListAsync(id);
            return RedirectToAction("UserToDoList");
        }
    }
}
