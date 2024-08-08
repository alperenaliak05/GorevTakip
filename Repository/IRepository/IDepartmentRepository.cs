using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepository
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentWithUsersAsync(int id);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department department);
        ToTask UpdateAsync(Department department);
        ToTask DeleteAsync(int id);
    }
}
