using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.IReporsitory;
using Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


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

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
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

        // Session kullanarak JWT token'ı sakla (isteğe bağlı)
        _httpContextAccessor.HttpContext.Session.SetString("JwtToken", tokenString);

        return new APIResponse
        {
            IsSuccess = true,
            Result = new LoginResponseDTO
            {
                Token = tokenString
            }
        };
    }
}
