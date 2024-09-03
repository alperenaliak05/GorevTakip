using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Data;
using TaskApp_Web.Models;

namespace TaskApp_Web.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private readonly TaskApp_WebContext _context;

        public InformationRepository(TaskApp_WebContext context)
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
