using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TaskApp_Web.Models.DTO;
using TaskApp_Web.Services.IServices;

namespace TaskApp_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View("Login");
            }

            // Kullanıcı başarılı bir şekilde giriş yaptıktan sonra "Home" sayfasına yönlendiriliyor
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Cookie'deki token'ı sil
            HttpContext.Response.Cookies.Delete("JwtToken");

            // Session'daki token'ı sil
            HttpContext.Session.Clear();

            // Cookie tabanlı oturum kapatma işlemi
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}
