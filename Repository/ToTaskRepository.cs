using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ToTaskRepository : IRepository<ToTask>
    {
        private readonly TaskAppContext _context;

        public ToTaskRepository(TaskAppContext context)
        {
            _context = context;
        }

        public Task<ToTask> AddAsync(ToTask entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToTask>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ToTask> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ToTask entity)
        {
            throw new NotImplementedException();
        }
    }
}