using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllWithDepartment();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> GetUserWithTasksAsync(int id);
        Task<IEnumerable<User>> GetAllWithTasksAndDepartment();
    }
}
