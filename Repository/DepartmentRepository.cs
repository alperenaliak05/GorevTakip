using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;
using System.Linq.Expressions;

namespace Repositories
{
    public class DepartmentRepository : Repository<Departments>, IDepartmentRepository
    {
        public DepartmentRepository(TaskAppContext context) : base(context)
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

        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
              .Select(d => new DepartmentViewModel
              {
                  Id = d.Id,
                  Name = d.Name,
              }).ToListAsync();
        }
    }
}
