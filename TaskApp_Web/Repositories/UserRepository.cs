using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskApp_WebContext _context;

    public UserRepository(TaskApp_WebContext context)
    {
        _context = context;
    }

    public async Task<Users> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Users> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<Users>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public IEnumerable<Users> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Users>> GetUsersByDepartmentAsync(int departmentId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsEmailAlreadyRegisteredAsync(string email)
    {
        throw new NotImplementedException();
    }
}
