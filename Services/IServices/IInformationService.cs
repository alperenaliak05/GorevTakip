using Models;

namespace Services.IServices
{
    public interface IInformationService
    {
        Task<IEnumerable<Information>> GetAllInformationsAsync();
        Task<Information> GetInformationByIdAsync(int id);
        Task<bool> AddInformationAsync(Information information);
        Task<bool> UpdateInformationAsync(Information information);
        Task<bool> DeleteInformationAsync(int id);
    }
}
