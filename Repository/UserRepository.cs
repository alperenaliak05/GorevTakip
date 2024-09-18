using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskAppContext _context;

        public UserRepository(TaskAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Department).ToListAsync();
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Department).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Department).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> AddUserAsync(Users user)
        {
            try
            {
                _context.Users.Add(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı eklenirken bir hata oluştu: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(Users user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.ProfilePicture = user.ProfilePicture ?? existingUser.ProfilePicture;
                existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
                existingUser.LastName = user.LastName ?? existingUser.LastName;
                existingUser.Email = user.Email ?? existingUser.Email;
                existingUser.PhoneNumber = user.PhoneNumber ?? existingUser.PhoneNumber;
                existingUser.WorkingHours = user.WorkingHours ?? existingUser.WorkingHours;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Departments>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<IEnumerable<Users>> GetUsersByDepartmentIdAsync(int departmentId) // Yeni metot
        {
            return await _context.Users.Where(u => u.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<Users> GetUserWithTasksAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Tasks)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Users>> GetAllWithTasksAndDepartment()
        {
            return await _context.Users
                .Include(u => u.Tasks)
                .Include(u => u.Department)
                .ToListAsync();
        }
        // Yeni eklenen metodlar
        public async Task<Users> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Department).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> AddAsync(Users user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

    }
}
