using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace TaskApp_Web.Controllers
{
   
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> UserProfile()
        {
            var userEmail = User.Identity.Name;
            var user = await _userRepository.GetUserByEmailAsync(userEmail);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Users model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                await _userRepository.UpdateUserAsync(user);

                return RedirectToAction("UserProfile");
            }

            return View("UserProfile", model);
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("AllUsers");
        }
    }
}
