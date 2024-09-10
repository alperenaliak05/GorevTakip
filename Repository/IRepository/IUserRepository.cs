using Models;

namespace Repositories.IReporsitory
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
        Task<IEnumerable<Users>> GetUsersByDepartmentIdAsync(int departmentId);
        Task<Users> GetUserWithTasksAsync(int id);
        Task<IEnumerable<Users>> GetAllWithTasksAndDepartment();
        Task<Users> GetByIdAsync(int id); 
        Task<bool> AddAsync(Users user);
        Task<bool> UpdateAsync(Users user); 
        Task<bool> DeleteAsync(int id); 


    }
}
