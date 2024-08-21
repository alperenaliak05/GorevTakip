﻿using TaskApp_Web.Data;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
namespace TaskApp_Web.Repositories
{
    public class UserToDoListRepository : IUserToDoListRepository
    {
        private readonly TaskApp_WebContext _context;

        public UserToDoListRepository(TaskApp_WebContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserToDoList>> GetToDoListsByUserIdAsync(int userId)
        {
            return await _context.UserToDoLists.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddToDoListAsync(UserToDoList toDoList)
        {
            _context.UserToDoLists.Add(toDoList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateToDoListAsync(UserToDoList toDoList)
        {
            _context.UserToDoLists.Update(toDoList);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteToDoListAsync(int id)
        {
            var toDoList = await _context.UserToDoLists.FindAsync(id);
            if (toDoList != null)
            {
                _context.UserToDoLists.Remove(toDoList);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }

}
