using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskAppContext _context;

        public UserRepository(TaskAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllWithTasksAndDepartment()
        {
            
            return await _context.Users
                .Include(u => u.Department)
                .Include(u => u.Tasks)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Department)
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserWithTasksAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Department)
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<User>> GetAllWithDepartment()
        {
            throw new NotImplementedException();
        }
    }
}
