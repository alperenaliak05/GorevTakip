using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskApp_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IToDoTaskService _taskService;

        public AccountController(IAuthService authService, IConfiguration configuration, IToDoTaskService taskService)
        {
            _authService = authService;
            _configuration = configuration;
            _taskService = taskService;
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

            // Kullanıcı ID'si ve varsa bir taskId alın (örneğin en son atanmış görev)
            int userId = response.UserId ?? 0;  // Nullable tipin default değeri null ise, 0 olarak ayarlanır.

            // Kullanıcının en son atanmış görevini al
            var userTasks = await _taskService.GetTasksByUserIdAsync(userId); // Kullanıcıya ait görevleri al
            int? latestTaskId = userTasks.OrderByDescending(t => t.DueDate).FirstOrDefault()?.Id; // Son görevi al

            // JWT token oluştur
            string token = GenerateJwtToken(userId.ToString(), latestTaskId);

            // Token'ı HttpOnly cookie olarak ayarla
            Response.Cookies.Append("JwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(30),
                Secure = true, // HTTPS gerektirir
                SameSite = SameSiteMode.Strict
            });

            // Kullanıcı başarılı bir şekilde giriş yaptıktan sonra "Home" sayfasına yönlendiriliyor
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Antiforgery");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete("JwtToken");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        private string GenerateJwtToken(string userId, int? taskId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (taskId.HasValue)
            {
                claims.Add(new Claim("taskId", taskId.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
