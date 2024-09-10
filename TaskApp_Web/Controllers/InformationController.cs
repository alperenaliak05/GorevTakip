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

        public InformationController(IInformationRepository informationRepository)
        {
            _informationRepository = informationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var informations = await _informationRepository.GetAllInformationsAsync();
            return View(informations);
        }

        [Authorize]
        public IActionResult CreateInformation()
        {
            var userDepartment = User.FindFirst("Department")?.Value;
            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Unauthorized(); 
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateInformation(Information model)
        {
            var userDepartment = User.FindFirst("Department")?.Value;
            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.CreatedByUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
                await _informationRepository.AddInformationAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditInformation(int id)
        {
            var userDepartment = User.FindFirst("Department")?.Value;
            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Unauthorized();
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
        public async Task<IActionResult> EditInformation(Information model)
        {
            var userDepartment = User.FindFirst("Department")?.Value;
            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                await _informationRepository.UpdateInformationAsync(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            var userDepartment = User.FindFirst("Department")?.Value;
            if (userDepartment != "İnsan Kaynakları Bilgilendirme")
            {
                return Unauthorized();
            }

            await _informationRepository.DeleteInformationAsync(id);
            return RedirectToAction("Index");
        }
    }
}
