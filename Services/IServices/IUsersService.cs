using Models;

namespace Services.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> GetUserByIdAsync(int id);
        Task<bool> AddUserAsync(Users user);
        Task<bool> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(int id);
    }
}
