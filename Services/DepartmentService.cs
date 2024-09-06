using Models;
using Repositories.IReporsitory;
using Services.IServices;

namespace Services
{
    public class DepartmentsService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<bool> AddDepartmentAsync(Departments department)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Departments>> GetAllDepartmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Departments> GetDepartmentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDepartmentAsync(Departments department)
        {
            throw new NotImplementedException();
        }
    }
}
