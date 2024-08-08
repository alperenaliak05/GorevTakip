using Data;
using Models;
using Repositories.IRepository;


namespace Repositories
{

    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly TaskAppContext _context;

        public DepartmentRepository(TaskAppContext context) : base(context)
        {
            _context = context;
        }

        public Task<Department> CreateAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public ToTask DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetDepartmentWithUsersAsync(int id)
        {
            throw new NotImplementedException();
        }

        ToTask IDepartmentRepository.UpdateAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }

}