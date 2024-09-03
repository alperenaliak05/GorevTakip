using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
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
