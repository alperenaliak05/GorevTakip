﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TaskAppWeb.Models.DTO;
using TaskAppWeb.Services.IServices;

namespace TaskAppWeb.Controllers
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
            // Session'daki tüm verileri temizle
            HttpContext.Session.Clear();

            // Cookie'deki JWT token'ı ve diğer oturum cookie'lerini sil
            HttpContext.Response.Cookies.Delete(".AspNetCore.Antiforgery");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete("JwtToken");

            // Cookie tabanlı oturum kapatma işlemi
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

    }
}
