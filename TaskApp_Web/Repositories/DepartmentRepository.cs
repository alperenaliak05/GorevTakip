using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskApp_Web.Data;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public class DepartmentRepository : Repository<Departments>, IDepartmentRepository
    {
        public DepartmentRepository(TaskApp_WebContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Departments>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Departments> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<IEnumerable<Departments>> FindAsync(Expression<Func<Departments, bool>> predicate)
        {
            return await _context.Departments.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Departments entity)
        {
            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Departments entity)
        {
            _context.Departments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
