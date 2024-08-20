using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAppWeb.Models;
using TaskAppWeb.Repositories;
using TaskAppWeb.Services.IServices;

namespace TaskAppWeb.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> AddUserAsync(Users user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(Users user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
