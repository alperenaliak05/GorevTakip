using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;

namespace Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private readonly TaskAppContext _context;

        public InformationRepository(TaskAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Information>> GetAllInformationsAsync()
        {
            return await _context.Informations.Include(i => i.CreatedByUser).ToListAsync();
        }

        public async Task<Information> GetInformationByIdAsync(int id)
        {
            return await _context.Informations.FindAsync(id);
        }

        public async Task<bool> AddInformationAsync(Information information)
        {
            _context.Informations.Add(information);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateInformationAsync(Information information)
        {
            _context.Informations.Update(information);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInformationAsync(int id)
        {
            var information = await GetInformationByIdAsync(id);
            if (information != null)
            {
                _context.Informations.Remove(information);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
