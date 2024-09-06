using DTOs.TaskApp_WebDTO;

namespace Services.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> LoginAsync(LoginRequestDTO loginRequest);

    }
}
