using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUsers(); 
        Task<Users> GetUserByEmailAsync(string email);
        Task<IEnumerable<Users>> GetUsersByDepartmentAsync(int departmentId);
        Task<bool> IsEmailAlreadyRegisteredAsync(string email);
        Task<Users> GetUserByIdAsync(int id);
        Task<IEnumerable<Users>> GetAllUsersAsync();
    }
}
