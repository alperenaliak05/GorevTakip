using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskAppWeb.Models;
using TaskAppWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace TaskAppWeb.Controllers
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
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            ViewBag.UserDepartment = currentUser.Department?.Name;

            return View(users);
        }

        [Route("Users/UserProfile/{id?}")]
        public async Task<IActionResult> UserProfile(int? id)
        {
            if (id == null)
            {
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

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var departments = await _userRepository.GetAllDepartmentsAsync();
            var model = new CreateUserViewModel
            {
                Departments = new SelectList(departments, "Id", "Name") // Burada dönüşüm yapılıyor
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    DepartmentId = model.DepartmentId
                };

                bool isAdded = await _userRepository.AddUserAsync(user);
                if (isAdded)
                {
                    return RedirectToAction("AllUsers");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı eklenirken bir hata oluştu.");
                }
            }

            var departments = await _userRepository.GetAllDepartmentsAsync();
            model.Departments = new SelectList(departments, "Id", "Name");
            return View(model);
        }

    }
}
