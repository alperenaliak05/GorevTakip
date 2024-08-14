using System.Threading.Tasks;
using TaskApp_Web.Models.DTO;

namespace TaskApp_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> LoginAsync(LoginRequestDTO loginRequest);
        Task<APIResponse> RegisterAsync(RegistrationRequestDTO registrationRequest);
    }
}
