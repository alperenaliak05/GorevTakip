using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> GetUserByIdAsync(int id);
        Task<Users> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(Users user);
        Task<bool> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(int id);
        Task<List<Departments>> GetAllDepartmentsAsync();
        Task<IEnumerable<Users>> GetUsersByDepartmentIdAsync(int departmentId); // Yeni metot eklendi
    }
}
