﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Data;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskApp_WebContext _context;

        public UserRepository(TaskApp_WebContext context)
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
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
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
    }
}