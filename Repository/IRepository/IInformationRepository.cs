using Models;

namespace Repositories.IReporsitory
{
    public interface IInformationRepository
    {
        Task<IEnumerable<Information>> GetAllInformationsAsync();
        Task<Information> GetInformationByIdAsync(int id);
        Task<bool> AddInformationAsync(Information information);
        Task<bool> UpdateInformationAsync(Information information);
        Task<bool> DeleteInformationAsync(int id);
    }
}
