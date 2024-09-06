using Models;

namespace Services.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Departments>> GetAllDepartmentsAsync();
        Task<Departments> GetDepartmentByIdAsync(int id);
        Task<bool> AddDepartmentAsync(Departments department);
        Task<bool> UpdateDepartmentAsync(Departments department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
