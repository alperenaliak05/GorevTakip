using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskApp_Web.Models.DTO;
using TaskApp_Web.Repositories;
using TaskApp_Web.Services.IServices;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<APIResponse> LoginAsync(LoginRequestDTO loginRequest)
    {
        // Kullanıcının e-postasına göre kullanıcı bilgilerini veritabanından al
        var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            return new APIResponse
            {
                IsSuccess = false,
                ErrorMessage = "Kullanıcı bulunamadı."
            };
        }

        // Kullanıcı doğrulandıysa, JWT token oluştur
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Kullanıcıyı cookie tabanlı olarak oturum açmış olarak işaretleyin
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        // JWT token'ı session'a kaydet
        _httpContextAccessor.HttpContext.Session.SetString("JwtToken", tokenString);

        // JWT token'ı cookie'ye kaydet
        _httpContextAccessor.HttpContext.Response.Cookies.Append("JwtToken", tokenString, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]))
        });

        return new APIResponse
        {
            IsSuccess = true,
            Result = new LoginResponseDTO
            {
                Token = tokenString
            }
        };
    }

    public Task<APIResponse> RegisterAsync(RegistrationRequestDTO registrationRequest)
    {
        throw new NotImplementedException();
    }
}
