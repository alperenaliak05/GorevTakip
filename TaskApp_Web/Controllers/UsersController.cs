using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Repositories.IReporsitory;
using Services.IServices;
using System.Security.Claims;

namespace TaskApp_Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBadgeService _badgeService;

        public UsersController(IUserRepository userRepository, IBadgeService badgeService)
        {
            _userRepository = userRepository;
            _badgeService = badgeService;
        }

        public async Task<IActionResult> AllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (currentUser != null)
            {
                ViewBag.UserDepartment = currentUser.Department?.Name;
            }
            else
            {
                ViewBag.UserDepartment = "Bilinmiyor";
            }

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


                var userBadges = await _badgeService.GetUserBadgesAsync(user.Id);
                var badges = userBadges.Select(ub => ub.Badge);

                var model = new UserProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DepartmentName = user.Department?.Name,
                    PhoneNumber = user.PhoneNumber,
                    WorkingHours = user.WorkingHours,
                    ProfilePicture = user.ProfilePicture,
                    UserBadges = badges,
                    Status = user.Status
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
                    PhoneNumber = user.PhoneNumber,
                    WorkingHours = user.WorkingHours,
                    ProfilePicture = user.ProfilePicture,
                    Status = user.Status
                };

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model, IFormFile profilePicture)
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
                user.PhoneNumber = model.PhoneNumber;
                user.WorkingHours = model.WorkingHours;

                if (profilePicture != null && profilePicture.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/uploads", profilePicture.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(stream);
                    }
                    user.ProfilePicture = "/uploads/" + profilePicture.FileName;
                }

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
                Departments = new SelectList(departments, "Id", "Name")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model, IFormFile profilePicture)
        {
            if (ModelState.IsValid)
            {
                string profilePicturePath = null;

                if (profilePicture != null && profilePicture.Length > 0)
                {
                    var uploadsFolder = Path.Combine("wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    profilePicturePath = Path.Combine(uploadsFolder, profilePicture.FileName);
                    using (var fileStream = new FileStream(profilePicturePath, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(fileStream);
                    }
                    profilePicturePath = "/uploads/" + profilePicture.FileName;
                }

                var user = new Users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    DepartmentId = model.DepartmentId,
                    PhoneNumber = model.PhoneNumber,
                    WorkingHours = model.WorkingHours,
                    Gender = model.Gender,
                    ProfilePicture = profilePicturePath
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

        public async Task<IActionResult> Badges()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userBadges = await _badgeService.GetUserBadgesAsync(userId);
            var availableBadges = await _badgeService.GetAvailableBadgesAsync(userId);

            var model = new UserBadgesViewModel
            {
                UserBadges = userBadges,
                AvailableBadges = availableBadges
            };

            return View("Badges", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(UsersStatus status)
        {
            var user = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            user.Status = status;
            await _userRepository.UpdateUserAsync(user);

            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (currentUser == null || currentUser.Department?.Name != "İnsan Kaynakları Uzmanı")
            {
                return Forbid();
            }

            var userToDelete = await _userRepository.GetUserByIdAsync(id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("AllUsers");
        }

    }
}
