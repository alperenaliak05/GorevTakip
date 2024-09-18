using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.IReporsitory;

namespace TaskApp_Web.Controllers
{
    [Authorize]
    public class InformationController : Controller
    {
        private readonly IInformationRepository _informationRepository;
        private readonly IUserRepository _userRepository;

        public InformationController(IInformationRepository informationRepository, IUserRepository userRepository)
        {
            _informationRepository = informationRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var informations = await _informationRepository.GetAllInformationsAsync();

            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            ViewBag.UserDepartment = currentUser?.Department?.Name ?? "Bilinmiyor";

            return View(informations);
        }

        [Authorize]
        public async Task<IActionResult> CreateInformation()
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            var userDepartment = currentUser?.Department?.Name;

            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Forbid();
            }

            var model = new Information();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInformation(Information model)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            var userDepartment = currentUser?.Department?.Name;

            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.CreatedByUserId = currentUser.Id;

                bool result = await _informationRepository.AddInformationAsync(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Bilgilendirme başarıyla kaydedildi!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Bilgi kaydedilirken bir hata oluştu.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Girdiğiniz bilgiler geçerli değil.");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditInformation(int id)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            var userDepartment = currentUser?.Department?.Name;

            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Forbid();
            }

            var information = await _informationRepository.GetInformationByIdAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInformation(Information model)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            var userDepartment = currentUser?.Department?.Name;

            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                await _informationRepository.UpdateInformationAsync(model);
                TempData["SuccessMessage"] = "Bilgilendirme başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);
            var userDepartment = currentUser?.Department?.Name;

            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Forbid();
            }

            await _informationRepository.DeleteInformationAsync(id);
            TempData["SuccessMessage"] = "Bilgilendirme başarıyla silindi!";
            return RedirectToAction("Index");
        }
    }
}
