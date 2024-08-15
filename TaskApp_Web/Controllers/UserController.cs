using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace TaskApp_Web.Controllers
{
    [Authorize]
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

        [Route("Users/UserProfile/{id?}")]
        public async Task<IActionResult> UserProfile(int? id)
        {
            if (id == null)
            {
                // Eğer id parametresi gelmemişse, giriş yapan kullanıcının profilini göster
                var userEmail = User.Identity.Name;
                var user = await _userRepository.GetUserByEmailAsync(userEmail);

                if (user == null)
                {
                    return NotFound();
                }

                var model = new UserProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DepartmentName = user.Department?.Name,
                };

                return View(model);
            }
            else
            {
                // Eğer id parametresi varsa, ilgili kullanıcının profilini göster
                var user = await _userRepository.GetUserByIdAsync(id.Value);

                if (user == null)
                {
                    return NotFound();
                }

                var model = new UserProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DepartmentName = user.Department?.Name,
                };

                return View(model);
            }
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

                return RedirectToAction("UserProfile", new { id = user.Id });
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
