using Models;

namespace Repositories.IReporsitory
{
    public interface IDepartmentRepository : IRepository<Departments>
    {
        Task<IEnumerable<DepartmentViewModel>> GetAllDepartmentsAsync();
    }
}
