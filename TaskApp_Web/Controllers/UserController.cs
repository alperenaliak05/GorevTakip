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

        [Authorize]
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

            // Kullanıcının verilerini UserProfileViewModel'e aktar
            var model = new UserProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DepartmentName = user.Department?.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

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
