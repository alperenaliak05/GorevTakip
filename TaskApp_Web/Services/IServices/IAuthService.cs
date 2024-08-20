using System.Threading.Tasks;
using TaskAppWeb.Models.DTO;

namespace TaskAppWeb.Services.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> LoginAsync(LoginRequestDTO loginRequest);
       
    }
}
